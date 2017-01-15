﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SqlSugar
{
    public class ExpressionResult
    {
        public ExpressionParameter CurrentParameter { get; set; }
        #region constructor
        private ExpressionResult()
        {
        }
        public ExpressionResult(ResolveExpressType resolveExpressType)
        {
            this._ResolveExpressType = resolveExpressType;
        }
        #endregion

        #region Fields
        private ResolveExpressType _ResolveExpressType;
        private StringBuilder _Result;
        #endregion

        #region properties
        private StringBuilder Result
        {
            get
            {
                if (_Result == null) _Result = new StringBuilder();
                return _Result;
            }

            set
            {
                _Result = value;
            }
        }
        #endregion
        public string GetString()
        {
            if (_Result == null) return null;
            return _Result.ToString().TrimEnd(',');
        }
        #region functions
        public string[] GetResultArray()
        {
            if (this._Result == null) return null;
            return this.Result.ToString().Split(',');
        }

        public string GetResultString()
        {
            if (this._Result == null) return null;
            return this.Result.ToString();
        }

        public void Append(object parameter)
        {
            if (this.CurrentParameter.IsValuable() && this.CurrentParameter.AppendType.IsIn(ExpressionResultAppendType.AppendTempDate)) {
                this.CurrentParameter.CommonTempData = parameter;
                return;
            }
            switch (this._ResolveExpressType)
            {
                case ResolveExpressType.SelectSingle:
                case ResolveExpressType.SelectMultiple:
                    parameter = parameter + ",";
                    break;
                case ResolveExpressType.WhereSingle:
                    break;
                case ResolveExpressType.WhereMultiple:
                    break;
                case ResolveExpressType.FieldSingle:
                    break;
                case ResolveExpressType.FieldMultiple:
                    break;
                default:
                    break;
            }
            this.Result.Append(parameter);
        }

        public void AppendFormat(string parameter, params object[] orgs)
        {
            if (this.CurrentParameter.IsValuable() && this.CurrentParameter.AppendType.IsIn(ExpressionResultAppendType.AppendTempDate))
            {
                this.CurrentParameter.CommonTempData = new KeyValuePair<string,object[]>(parameter,orgs);
                return;
            }
            switch (this._ResolveExpressType)
            {
                case ResolveExpressType.SelectSingle:
                case ResolveExpressType.SelectMultiple:
                    parameter = parameter + ",";
                    break;
                case ResolveExpressType.WhereSingle:
                    break;
                case ResolveExpressType.WhereMultiple:
                    break;
                case ResolveExpressType.FieldSingle:
                    break;
                case ResolveExpressType.FieldMultiple:
                    break;
                default:
                    break;
            }
            this.Result.AppendFormat(parameter, orgs);
        }

        public void Replace(string parameter, string newValue)
        {
            this.Result.Replace(parameter, newValue);
        }
        #endregion
    }
}