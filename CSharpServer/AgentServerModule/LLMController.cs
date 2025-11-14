using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentServerModule
{
    public sealed class ObjectController
    {
        private ConcurrentQueue<LLMUser> llmQueue = new ConcurrentQueue<LLMUser>();
        //private AgentServer agentServer = null;
        private static ObjectController instance = null;
        public ObjectController()
        {
        }

        public AgentServer AgentServer { get; set; }


        public static ObjectController Instance()
        {
            if (instance == null)
                instance = new ObjectController();

            return instance;
        }
            

        public LLMUser Dequeue()
        {
            if (this.llmQueue.TryDequeue(out var llm))
            {
                //this.ConitnuePost(checktask);
                return llm;
            }

            return null;
        }

        public void Enqueue(LLMUser llm)
        {
            this.llmQueue.Enqueue(llm);
        }


    }
}
