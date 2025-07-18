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

namespace Atom.BehaviorTree
{
    public abstract class DecoratorTaskProcessor : ContainerTaskProcessor
    {
        protected TaskProcessor Child
        {
            get { return Children.Count == 0 ? null : Children[0]; }
        }

        protected DecoratorTaskProcessor(Task model) : base(model)
        {
            AddPort(new PortProcessor(TaskProcessor.ParentPortName, BasePort.Direction.Top, BasePort.Capacity.Single, typeof(ContainerTaskProcessor)));
            AddPort(new PortProcessor(TaskProcessor.ChildrenPortName, BasePort.Direction.Bottom, BasePort.Capacity.Single, typeof(TaskProcessor)));
        }
    }
}