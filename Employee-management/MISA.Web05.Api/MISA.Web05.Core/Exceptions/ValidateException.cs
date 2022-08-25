using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05.Core.Exceptions
{
    public class ValidateException: Exception
    {
        public string? ValidateErrorMsg { get; set; }
        public IDictionary Errors { get; set; }
        public ValidateException(string Msg)
        {
            ValidateErrorMsg = Msg;
        }
        public ValidateException(List<string> errorMsg)
        {
            Errors = new Dictionary<string, object>();
            Errors.Add("erros",errorMsg);
        }
        public override string Message => this.ValidateErrorMsg;
        public override IDictionary Data => Errors;
    }
}
