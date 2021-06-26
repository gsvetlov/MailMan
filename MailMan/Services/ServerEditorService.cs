using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MailMan.Models;
using MailMan.Services.EntityEditorService;
using MailMan.ViewModels.UserDialog;
using MailMan.Views.UserDialog;

namespace MailMan.Services
{
    class ServerEditorService : IEntityEditorService<Server>
    {
        public bool Edit(Server server)
        {
            EditServerDialogVM model = new()
            {
                Name = server.Name,
                Address = server.Address,
                Port = server.Port,
                UseSSL = server.UseSSL,
                Login = server.Login,
                Password = server.Password,
                Description = server.Description
            };

            ServerEditView view = new()
            {
                DataContext = model
            };

            model.EditSubmitted += (s, e) =>
            {
                view.DialogResult = true;
                view.Close();
            };

            model.EditCancelled += (s, e) =>
            {
                view.DialogResult = false;
                view.Close();
            };

            if (view.ShowDialog() is false) return false;

            server.Name = model.Name;
            server.Address = model.Address;
            server.Port = model.Port;
            server.UseSSL = model.UseSSL;
            server.Login = model.Login;
            server.Password = model.Password;
            server.Description = model.Description;

            return true;
        }
    }
}
