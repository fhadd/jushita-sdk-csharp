using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Top.Api.Stream;
using System.Threading;

namespace Top.Api.Jushita.stream
{
    public class JushitaTopCometStreamImpl : TopCometStreamImpl
    {
        private JushitaConfigurationV2 _configuration;
        public JushitaConfigurationV2 configuration
        {
            get { return _configuration; }
            set { _configuration = value; }
        }

        public JushitaTopCometStreamImpl(JushitaConfigurationV2 configuration)
            : base(configuration)
        {
            this.configuration = configuration;
        }

        public new void Start()
        {
            base.Start();
            if (configuration.topCometMessageListener == null)
                throw new NullReferenceException("comet message  listener must not be null");
            configuration.driver.start();
        }

        public new void Stop()
        {
            base.Stop();
            configuration.driver.stop();
        }
    }
}
