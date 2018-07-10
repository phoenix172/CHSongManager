using System.ComponentModel;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using CHSongManager.Services.Interfaces;
using CHSongManager.ViewModels.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace CHSongManager.Tests.Helpers
{
    public class PropertyChangedAssert
    {
        public static void NotificationPropagates(INotifyPropertyChanged sourceObject, string sourceProperty,
            INotifyPropertyChanged targetObject, string targetProperty)
        {
            string raisedForProperty = null;
            targetObject.PropertyChanged += (s, e) => raisedForProperty = e.PropertyName;
            sourceObject.PropertyChanged +=
                Raise.Event<PropertyChangedEventHandler>(sourceObject, new PropertyChangedEventArgs(sourceProperty));
            Assert.That(raisedForProperty, Is.EqualTo(targetProperty));
        }
    }
}