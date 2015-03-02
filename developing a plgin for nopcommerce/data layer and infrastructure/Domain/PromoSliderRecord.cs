using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widget.PromoSlider.Domain
{
    public class PromoSliderRecord : BaseEntity
    {
        public PromoSliderRecord()
        {
            Images = new List<PromoImageRecord>();
        }

        public virtual int PromoSliderId { get; set; }
        public virtual string PromoSliderName { get; set; }
        public bool IsActive { get; set; }
        public virtual string ZoneName { get; set; }
        public virtual int Interval { get; set; }
        public virtual bool PauseOnHover { get; set; }
        public virtual bool Wrap { get; set; }
        public virtual bool KeyBoard { get; set; }

        public virtual List<PromoImageRecord> Images { get; set; }
    }
}
