
using System;
using System.Collections.Concurrent;

namespace Common
{
    public sealed class TaskJob
    {
        public Action Func;

        //public ConcurrentQueue<TaskJob> TaskQueue = new ConcurrentQueue<TaskJob>();

        public TaskJob()
        {

        }

        public TaskJob(Action func)
        {
            this.Func = func;
        }

        //public void ThreadPoolCallback(Object threadContext)
        //{
        //    var taskSyncer = threadContext as TaskSyncer;

        //    taskSyncer.CurTaskJob.Action();

        //    taskSyncer.CurTaskJob.AfterRun(taskSyncer);
        //}

        public void Action()
        {
            Func();
        }

        //public void AfterRun(TaskSyncer taskSyncer)
        //{
        //    if (taskSyncer.TryPop(out var task))
        //    {
        //        taskSyncer.ConitnuePost(task);
        //    }
        //    else
        //    {
        //        taskSyncer.EndWork(this);
        //    }
        //}

        //public void AfterRun(TaskSyncer taskSyncer)
        //{
        //    if (taskSyncer.TryPop(out var task))
        //    {
        //        taskSyncer.ConitnuePost(task);
        //    }
        //    else
        //    {
        //        taskSyncer.EndWork(this);
        //    }
        //}
    };
}