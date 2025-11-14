using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Common
{
    public class TaskSyncer
    {
        public volatile TaskJob CurTaskJob;
        //public Atomic<TaskJob> Working;
        //std::atomic<Task*> WorkingAdress;
        ConcurrentQueue<TaskJob> TaskQueue = new ConcurrentQueue<TaskJob>();
        //Concurrency::concurrent_queue<Task*> TaskList;

        //public:
        public TaskSyncer()
        {
            //WorkingAdress = nullptr;
        }
        ~TaskSyncer() { }

        public void ThreadPoolCallback(Object threadContext)
        {
            var taskJob = threadContext as TaskJob;

            taskJob.Action();

            this.AfterRun();
        }

        public void Post(TaskJob task)
        {
            if (Interlocked.CompareExchange<TaskJob>(ref CurTaskJob, task, null) == null || this.AddChild(task) == false)
            {
                ThreadPool.QueueUserWorkItem(this.ThreadPoolCallback, CurTaskJob);
            }
        }

        public bool AddChild(TaskJob task)
        {
            if (Interlocked.CompareExchange<TaskJob>(ref this.CurTaskJob, task, null) == null)
            {
                return false;
            }

            //if (this.CurTaskJob == null)
            //{
            //    Console.WriteLine("CurTaskJob is null and TaskQueue.Enqueue {0}", this.TaskQueue.Count);
            //    Interlocked.Exchange(ref this.CurTaskJob, task);

            //    return false;
            //}

            this.TaskQueue.Enqueue(task);

            if (this.CurTaskJob == null && this.TaskQueue.Count > 0)
            {
                Console.WriteLine("After CurTaskJob is null and TaskQueue.Enqueue {0}", this.TaskQueue.Count);
                this.CheckConitnuePost();
            }

            return true;
        }

        public void ConitnuePost(TaskJob task)
        {
            Interlocked.Exchange(ref CurTaskJob, task);
            ThreadPool.QueueUserWorkItem(this.ThreadPoolCallback, CurTaskJob);

            // 만약 스레드 재사용을 하려면 이게 되지만 호출스택이 무한으로길어질수도
            //CurTaskJob.ThreadPoolCallback(this);
        }

        public bool CheckConitnuePost()
        {
            if (this.TaskQueue.TryDequeue(out var checktask))
            {
                this.ConitnuePost(checktask);
                return true;
            }

            return false;
        }

        public void AfterRun()
        {
            if (this.CheckConitnuePost() == false)
            {
                if (this.TaskQueue.Count > 0)
                {
                    Console.WriteLine("Before this.CurTaskJob is null {0}", this.TaskQueue.Count);
                    this.CheckConitnuePost();

                    return;
                }

                Interlocked.Exchange(ref this.CurTaskJob, null);

                if (CurTaskJob == null && this.TaskQueue.Count > 0)
                {
                    Console.WriteLine("After this.CurTaskJob is null {0}", this.TaskQueue.Count);
                    this.CheckConitnuePost();
                }
            }
        }
    };
}