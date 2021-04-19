using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace UniversalModal.WPF.Helpers
{
    internal static class VisualTreeHelperExtensions
    {
        public static T FindVisualParent<T>([CanBeNull] this DependencyObject obj) where T : class
        {
            if (obj is null) return null;
            var target = obj;
            do { target = VisualTreeHelper.GetParent(target); } while (target != null && !(target is T));
            return target as T;
        }

        [CanBeNull]
        public static DependencyObject FindLogicalRoot([CanBeNull] this DependencyObject obj)
        {
            if (obj is null) return null;
            do
            {
                var parent = LogicalTreeHelper.GetParent(obj);
                if (parent is null) return obj;
                obj = parent;
            } while (true);
        }

        [CanBeNull]
        public static DependencyObject FindVisualRoot([CanBeNull] this DependencyObject obj)
        {
            if (obj is null) return null;
            do
            {
                var parent = VisualTreeHelper.GetParent(obj);
                if (parent is null) return obj;
                obj = parent;
            } while (true);
        }

        public static T FindLogicalParent<T>([CanBeNull] this DependencyObject obj) where T : class
        {
            if (obj is null) return null;
            var target = obj;
            do { target = LogicalTreeHelper.GetParent(target); } while (target != null && !(target is T));
            return target as T;
        }

        public static IEnumerable<DependencyObject> GetAllVisualChlds([CanBeNull] this DependencyObject obj)
        {
            if (obj is null) yield break;
            var to_process = new Stack<DependencyObject>(obj.GetVisualChilds());
            do
            {
                obj = to_process.Pop();
                yield return obj;
                obj.GetVisualChilds().ForEach(to_process.Push);
            } while (to_process.Count > 0);
        }

        public static IEnumerable<DependencyObject> GetVisualChilds([CanBeNull] this DependencyObject obj)
        {
            if (obj is null) yield break;
            for (int i = 0, count = VisualTreeHelper.GetChildrenCount(obj); i < count; i++)
                yield return VisualTreeHelper.GetChild(obj, i);
        }

        public static IEnumerable<DependencyObject> GetAllLogicalChlds([CanBeNull] this DependencyObject obj)
        {
            if (obj is null) yield break;
            var to_process = new Stack<DependencyObject>(obj.GetLogicalChilds());
            do
            {
                obj = to_process.Pop();
                yield return obj;
                obj.GetLogicalChilds().ForEach(to_process.Push);
            } while (to_process.Count > 0);
        }

        public static IEnumerable<DependencyObject> GetLogicalChilds([CanBeNull] this DependencyObject obj) => obj is null
            ? Enumerable.Empty<DependencyObject>()
            : LogicalTreeHelper.GetChildren(obj).OfType<DependencyObject>();
    }
}
