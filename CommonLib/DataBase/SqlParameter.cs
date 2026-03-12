using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;
using System.Text;

namespace CommonLib.DataBase
{
    public class SqlParameter
    {
        private readonly string _parameterName;
        private readonly object _value;

        // 생성자 
        public SqlParameter(string parameterName, object value)
        {
            this._parameterName = parameterName;
            this._value = value;
        }

        public string ParameterName => _parameterName;

        public object Value => _value;
    }
}
