// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC
// *************************************************

using Undersoft.SDK.Rubrics.Attributes;

namespace Undersoft.SVC.Service.Application.ViewModels
{
    public class Payment : DataObject, IViewModel
    {
        private double? _value;
        private double? _tax = 23;
        private double? _amount;

        public virtual string? Name { get; set; }

        [VisibleRubric]
        [RubricSize(8)]
        [DisplayRubric("Value")]
        public virtual double? Value { get => _value; set { _value = value; Compute(); } }

        [VisibleRubric]
        [RubricSize(4)]
        [DisplayRubric("Tax")]
        public virtual double? Tax { get => _tax; set { _tax = value; Compute(); } }

        [VisibleRubric]
        [RubricSize(8)]
        [DisplayRubric("Amount")]
        public virtual double? Amount { get => _amount; set { _amount = value; Compute(); } }

        private void Compute()
        {
            if (_amount != null && _tax != null)
            {
                _value = (_amount.Value / _tax.Value.ToMarkup()).Round(2);
            }
            else if (_value != null && _tax != null)
            {
                _amount = (_value.Value * _tax.Value.ToMarkup()).Round(2);
            }
            else if (_value != null && _amount != null)
            {
                _tax = (_amount.Value / _value.Value).FromMarkup(0);
            }
        }

        public virtual long? CertificateId { get; set; }
    }
}
