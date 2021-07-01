using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

using FileParserWPF.Data;
using FileParserWPF.Model;
using FileParserWPF.ViewModels.Base;

namespace FileParserWPF.ViewModels
{
    internal class MainVM : ViewModel
    {
        private ChunkedDataReader reader;
        private ChunkedFilter filter;
        private ChunkProcessor processor;
        private ChunkWriter writer;

        private StringBuilder messageSb = new();
        private Stopwatch sw = new();

        public MainVM()
        {
            writer = new ChunkWriter();
            processor = new ChunkProcessor(writer.Write);
            filter = new ChunkedFilter(processor.Enqueue);
            reader = new ChunkedDataReader(filter.Enque);
        }

        private string _InputDirectory = @".\input";
        public string InputDirectory { get => _InputDirectory; set => Set(ref _InputDirectory, value); }

        private string _OutputDirectory = @".\input\result.dat";
        public string OutputDirectory { get => _OutputDirectory; set => Set(ref _OutputDirectory, value); }

        private int _Samples = 9999;
        public int Samples { get => _Samples; set => Set(ref _Samples, value); }

        private string _MessageText = "Сообщения";
        public string MessageText { get => _MessageText; set => Set(ref _MessageText, value); }

        private ICommand _GenerateData;
        public ICommand GenerateData => _GenerateData ??= new LambdaCommand(OnGenerateDataCommandExecuted, CanGenerateDataCommandExecute);

        private bool _GenerateDataExecuting;
        public bool GenerateDataExecuting { get => _GenerateDataExecuting; set => Set(ref _GenerateDataExecuting, value); }
        private bool CanGenerateDataCommandExecute(object arg) => !GenerateDataExecuting;

        private CancellationTokenSource generateDataCommandCts;
        private async void OnGenerateDataCommandExecuted(object obj)
        {
            GenerateDataExecuting = true;
            generateDataCommandCts = new CancellationTokenSource();
            sw.Restart();

            await Task.Run(() => DataGenerator.GenerateData(InputDirectory, Samples, generateDataCommandCts.Token));

            sw.Stop();

            generateDataCommandCts.Dispose();
            GenerateDataExecuting = false;

            MessageText = messageSb.Clear().Append(MessageText).Append(Environment.NewLine).Append($"Данные сгенерированы за {sw.Elapsed}").ToString();
        }

        private ICommand _CancelGenerateData;
        public ICommand CancelGenerateData => _CancelGenerateData ??= new LambdaCommand(OnCancelGenerateDataExecuted, CanCancelGenerateDataExecute);

        private bool CanCancelGenerateDataExecute(object arg) => GenerateDataExecuting;
        private void OnCancelGenerateDataExecuted(object obj) => generateDataCommandCts?.Cancel();

        private ICommand _CancelProcessData;
        public ICommand CancelProcessData => _CancelProcessData ??= new LambdaCommand(OnCancelProcessDataExecuted, CanCancelProcessDataExecute);

        private bool CanCancelProcessDataExecute(object arg) => ProcessDataExecuting;
        private void OnCancelProcessDataExecuted(object obj) => processDataCommandCts?.Cancel();

        private bool _ProcessDataExecuting;
        public bool ProcessDataExecuting { get => _ProcessDataExecuting; set => Set(ref _ProcessDataExecuting, value); }

        private CancellationTokenSource processDataCommandCts;

        private ICommand _ProcessData;
        public ICommand ProcessData => _ProcessData ??= new LambdaCommand(OnProcessDataExecuted, CanProcessDataExecute);

        private bool CanProcessDataExecute(object arg) => !ProcessDataExecuting;
        private async void OnProcessDataExecuted(object obj)
        {
            ProcessDataExecuting = true;
            processDataCommandCts = new();

            sw.Restart();
            try
            {
                writer.Filename = OutputDirectory;
                await Task.Run(() => reader.ReadAsync(InputDirectory, processDataCommandCts.Token));
                await Task.Run(() => writer.Close());
            }
            catch (AggregateException e)
            {
                MessageText = messageSb.Clear().Append(MessageText).Append(Environment.NewLine).Append(e.InnerException.Message).ToString();
            }
            sw.Stop();

            processDataCommandCts.Dispose();
            ProcessDataExecuting = false;

            MessageText = messageSb.Clear().Append(MessageText).Append(Environment.NewLine).Append($"Данные обработаны за {sw.Elapsed}").ToString();
        }
    }
}
