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

#if UNITY_EDITOR
using Atom.GraphProcessor;
using Atom.GraphProcessor.Editors;
using UnityEditor.Experimental.GraphView;

namespace Atom.BehaviorTree.Editors
{
    public class BehaviorTreeGraphView : BaseGraphView
    {
        protected override void OnCreated()
        {
            base.OnCreated();
            schedule.Execute(UpdateState).Every(100);
        }

        private void UpdateState()
        {
            foreach (var node in NodeViews.Values)
            {
                if (node is TaskView taskView)
                {
                    taskView.UpdateState();
                }
            }
        }

        protected override void BuildNodeMenu(NodeMenuWindow nodeMenu)
        {
            foreach (var pair in GraphProcessorUtil.NodeStaticInfos)
            {
                if (!typeof(Task).IsAssignableFrom(pair.Key))
                    continue;

                var nodeType = pair.Key;
                var nodeStaticInfo = GraphProcessorUtil.NodeStaticInfos[nodeType];
                if (nodeStaticInfo.hidden)
                    continue;

                var path = nodeStaticInfo.path;
                var menu = nodeStaticInfo.menu;
                nodeMenu.entries.Add(new NodeMenuWindow.NodeEntry(path, menu, nodeType));
            }
        }

        protected override bool IsCompatible(BasePortView fromPortView, BasePortView toPortView, NodeAdapter nodeAdapter)
        {
            if (fromPortView.node == toPortView.node)
                return false;
            if (fromPortView.ViewModel.Owner == toPortView.ViewModel.Owner)
                return false;
            return base.IsCompatible(fromPortView, toPortView, nodeAdapter);
        }
    }
}
#endif