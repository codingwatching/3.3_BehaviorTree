#region 注 释

/***
 *
 *  Title:
 *  
 *  Description:
 *  
 *  Date:
 *  Version:
 *  Writer: 半只龙虾人
 *  Github: https://github.com/haloman9527
 *  Blog: https://www.haloman.net/
 *
 */

#endregion

using Atom.GraphProcessor;
using Atom;

namespace Atom.BehaviorTree
{
    [TaskIcon("BehaviorTree/Icons/Repeater")]
    [NodeMenu("Decorator/Repeater")]
    public class Repeater : Task
    {
        public bool ignoreFaild;
        public int loopCount;
    }

    [ViewModel(typeof(Repeater))]
    public class RepeaterProcessor : DecoratorTaskProcessor, IUpdateTask
    {
        private Repeater model;
        private int counter;
        private bool childRunning;

        public RepeaterProcessor(Repeater model) : base(model)
        {
            this.model = model;
        }

        protected override void DoStart()
        {
            if (model.loopCount != 0)
            {
                counter = 0;
                childRunning = true;
                Child.Start();
            }
            else
                SelfStop(true);
        }

        public void Update()
        {
            if (childRunning)
                return;

            childRunning = true;
            Child.Start();
        }

        protected override void OnChildStopped(TaskProcessor child, bool result)
        {
            this.childRunning = false;
            if (model.ignoreFaild ||　result)
            {
                if (model.loopCount > 0 && ++counter >= model.loopCount)
                    SelfStop(true);
            }
            else
                SelfStop(false);
        }
    }
}