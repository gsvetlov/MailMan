using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        public MainVM()
        {
            writer = new ChunkWriter(@"C:\temp\parser\output\result.dat");
            processor = new ChunkProcessor(writer.Write);
            filter = new ChunkedFilter(processor.Enqueue);
            reader = new ChunkedDataReader(filter.Enque);
        }

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
            await Task.WhenAll(DataGenerator.GenerateData(@"C:\temp\parser\input", 99, generateDataCommandCts.Token));
            generateDataCommandCts.Dispose();
            GenerateDataExecuting = false;
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
            var progress = new Progress<int>(value => BtContext = value.ToString());
            Debug.WriteLine($"sending processing request in {Thread.CurrentThread.ManagedThreadId}");
            await reader.ReadAsync(@"C:\temp\parser\input", progress);
            writer.Close();
            BtContext = "Process";
            ProcessDataExecuting = false;

        }

        private string _BtContext = "Process";
        public string BtContext { get => _BtContext; set => Set(ref _BtContext, value); }
    }
}
