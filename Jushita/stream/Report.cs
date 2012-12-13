using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Newtonsoft.Json;

namespace Top.Api.Jushita.stream
{
    public class Report
    {
        private List<string> _sOffset = new List<string>();
        public List<string> sOffset
        {
            get { return _sOffset; }
            set { _sOffset = value; }
        }

        private List<string> _fOffset = new List<string>();
        public List<string> fOffset
        {
            get { return _fOffset; }
            set { _fOffset = value; }
        }

        public string asJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

}
