using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailMan.Services.EMailAddressValidator
{
    public interface IEmailAddressValidator
    {
        bool Check(string address);
    }
}
