using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Browser
{
    public class Edg : BaseBrowser
    {
        private readonly string _agent;

        public Edg(string agent)
        {
            _agent = agent.ToLower();
            var edg = BrowserType.Edg.ToString().ToLower();

            if (_agent.Contains(edg))
            {
                var first = _agent.IndexOf(edg);
                var version = _agent.Substring(first + edg.Length + 1);
                Version = ToVersion(version);
                Type = BrowserType.Edg;
            }
        }

    }
}
