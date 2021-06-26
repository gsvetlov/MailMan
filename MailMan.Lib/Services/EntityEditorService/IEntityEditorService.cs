using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MailMan.Models.Base;

namespace MailMan.Services.EntityEditorService
{
    public interface IEntityEditorService<T> where T : Entity
    {
        bool Edit(T entity);

    }
}
