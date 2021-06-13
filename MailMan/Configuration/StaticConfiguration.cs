using System;

namespace MailMan.Configuration
{
    internal static class StaticConfiguration
    {
        internal static string SmtpHost { get => "smtp.yandex.ru"; }
        internal static int SmtpPort { get => 25; }
        internal static string From { get => "svetlov.georg@yandex.ru"; }
        internal static string To { get => "svetlov.georg@yandex.ru"; }
        internal static string Subject { get => $"Тестовое сообщение от {From} {DateTime.Now:yyyy-MM-dd HH:mm:ss.ff}"; }
        internal static string Body { get => @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean id sodales massa, vitae rhoncus leo. Fusce diam lorem, pulvinar ut ullamcorper non, iaculis sed quam. Mauris sagittis vulputate magna, id eleifend est sagittis in. Ut ultrices leo ut ultrices vestibulum. Nunc scelerisque, orci vitae commodo semper, erat nisi cursus arcu."; }
        internal static string Username { get => "svetlov.georg"; }
    }
}
