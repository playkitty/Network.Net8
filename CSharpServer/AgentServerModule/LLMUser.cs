using ServerModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentServerModule
{
    public sealed class LLMUser : User
    {
        //private bool active = false;
        public LLMUser()
        {
            //this.agentController.BindController(this);
        }
        public override void OnClosed()
        {
            this.Active = false;
        }

        public bool Active { get; set; } = false;

    }
}
