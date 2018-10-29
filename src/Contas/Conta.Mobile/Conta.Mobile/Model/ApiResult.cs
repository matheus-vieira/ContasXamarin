using System;
using System.Collections.Generic;
using System.Text;

namespace Conta.Mobile.Model
{
    public class ApiResult<T> where T : new()
    {
        public bool Ok { get; set; } = false;
        public string Message { get; set; } = string.Empty;

        public T Response { get; set; }
    }
}
