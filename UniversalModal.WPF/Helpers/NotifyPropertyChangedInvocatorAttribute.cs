using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalModal.WPF.Helpers
{
    /// <summary>
    /// Indicates that the method is contained in a type that implements
    /// <see cref="System.ComponentModel.INotifyPropertyChanged"/> interface
    /// and this method is used to notify that some property value changed
    /// </summary>
    /// <remarks>
    /// The method should be non-static and conform to one of the supported signatures:
    /// <list>
    /// <item><c>NotifyChanged(string)</c></item>
    /// <item><c>NotifyChanged(params string[])</c></item>
    /// <item><c>NotifyChanged{T}(Expression{Func{T}})</c></item>
    /// <item><c>NotifyChanged{T,U}(Expression{Func{T,U}})</c></item>
    /// <item><c>SetProperty{T}(ref T, T, string)</c></item>
    /// </list>
    /// </remarks>
    /// <example><code>
    /// public class Foo : INotifyPropertyChanged {
    ///   public event PropertyChangedEventHandler PropertyChanged;
    ///   [NotifyPropertyChangedInvocator]
    ///   protected virtual void NotifyChanged(string propertyName) { ... }
    ///
    ///   private string _name;
    ///   public string Name {
    ///     get { return _name; }
    ///     set { _name = value; NotifyChanged("LastName"); /* Warning */ }
    ///   }
    /// }
    /// </code>
    /// Examples of generated notifications:
    /// <list>
    /// <item><c>NotifyChanged("Property")</c></item>
    /// <item><c>NotifyChanged(() =&gt; Property)</c></item>
    /// <item><c>NotifyChanged((VM x) =&gt; x.Property)</c></item>
    /// <item><c>SetProperty(ref myField, value, "Property")</c></item>
    /// </list>
    /// </example>
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class NotifyPropertyChangedInvocatorAttribute : Attribute
    {
        public NotifyPropertyChangedInvocatorAttribute() { }
        public NotifyPropertyChangedInvocatorAttribute(string parameterName) => ParameterName = parameterName;

        public string ParameterName { get; }
    }
    /// <summary>
    /// Indicates that the value of the marked element could be <c>null</c> sometimes,
    /// so the check for <c>null</c> is necessary before its usage
    /// </summary>
    /// <example><code>
    /// [CanBeNull] public object Test() { return null; }
    /// public void UseTest() {
    ///   var p = Test();
    ///   var s = p.ToString(); // Warning: Possible 'System.NullReferenceException'
    /// }
    /// </code></example>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Delegate | AttributeTargets.Field)]
    internal sealed class CanBeNullAttribute : Attribute { }

    /// <summary>
    /// Indicates that the value of the marked element could never be <c>null</c>
    /// </summary>
    /// <example><code>
    /// [NotNull] public object Foo() {
    ///   return null; // Warning: Possible 'null' assignment
    /// }
    /// </code></example>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Delegate | AttributeTargets.Field)]
    internal sealed class NotNullAttribute : Attribute { }

    /// <summary>
    /// Can be applied to symbols of types derived from IEnumerable as well as to symbols of Task
    /// and Lazy classes to indicate that the value of a collection item, of the Task.Result property
    /// or of the Lazy.Value property can never be null.
    /// </summary>
    /// <example><code>
    /// public void Foo([ItemNotNull]List&lt;string&gt; books)
    /// {
    ///   foreach (var book in books) {
    ///     if (book != null) // Warning: Expression is always true
    ///      Console.WriteLine(book.ToUpper());
    ///   }
    /// }
    /// </code></example>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Delegate)]
    internal sealed class ItemNotNullAttribute : Attribute { }

    /// <summary>
    /// Can be applied to symbols of types derived from IEnumerable as well as to symbols of Task
    /// and Lazy classes to indicate that the value of a collection item, of the Task.Result property
    /// or of the Lazy.Value property can be null.
    /// </summary>
    /// <example><code>
    /// public void Foo([ItemCanBeNull]List&lt;string&gt; books)
    /// {
    ///   foreach (var book in books)
    ///   {
    ///     // Warning: Possible 'System.NullReferenceException'
    ///     Console.WriteLine(book.ToUpper());
    ///   }
    /// }
    /// </code></example>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Delegate)]
    internal sealed class ItemCanBeNullAttribute : Attribute { }
}
