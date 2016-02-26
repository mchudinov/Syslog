using System;
using System.Collections.Generic;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Text;
using Server;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;
using Microsoft.Xaml.Interactivity;
using MyToolkit.Collections;

namespace Gui
{
    //netsh http add urlacl url=http://+:8000/ServiceModelSamples/Service user=Everyone
    public sealed partial class MainPage : Page
    {
        public bool? Autoscroll { get; set; } = true;
        private readonly MtObservableCollection<string> _collection = new MtObservableCollection<string>();
        private readonly ObservableCollectionView<string> _collectionView;
        private static readonly IMessageParser _parser = new MessageParser();
        private static ISyslogListener _listener;
        public uint Port { get; set; } = 514;
        private const int PeriodUpdateView = 2000;
        
        public MainPage()
        {
            InitializeComponent();
            _collectionView = new ObservableCollectionView<string>(_collection);
            StartSyslogServer();
            ThreadPoolTimer.CreatePeriodicTimer(MyTimerElapsedHandler, TimeSpan.FromMilliseconds(PeriodUpdateView));
        }
        
        private async void MyTimerElapsedHandler(ThreadPoolTimer timer)
        {
            if (_listener.MessagesQueue.Count > 0)
            {
                var list = new List<string>();
                while (_listener.MessagesQueue.Count > 0)
                {
                    string temp;
                    _listener.MessagesQueue.TryDequeue(out temp);
                    if (!string.IsNullOrEmpty(temp))
                        list.Add(_parser.Parse(temp));
                }

                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                    _collection.AddRange(list);
                });
            }
            _listener.StartListener();
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _collectionView.Filter = c => c.ToLower().Contains(FilterTextBox.Text.ToLower());
        }

        private void ControlSyslog_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if ((bool)Autoscroll)
            {
                Scroller.ChangeView(0.0f, double.MaxValue, 1.0f);
            }
        }

        private void StartSyslogServer()
        {
            _listener = new DatagramListener(Port);
            _listener.StartListener();
        }

        private void CheckBoxAutoscroll_Checked(object sender, RoutedEventArgs e)
        {
            Autoscroll = ((CheckBox)sender).IsChecked;
        }
    }

    public class TextBlockHighlightBehaviour : DependencyObject, IBehavior
    {
        private EventHandler<EventArgs> targetUpdatedHandler;
        public List<Highlight> Highlights { get; set; }

        public TextBlockHighlightBehaviour()
        {
            this.Highlights = new List<Highlight>();
        }

        #region IBehaivior

        public DependencyObject AssociatedObject { get; set; }

        public void Attach(DependencyObject associatedObject)
        {
            AssociatedObject = associatedObject;
            var fe = AssociatedObject as FrameworkElement;
            if (fe != null)
            {
                
            }
        }

        //protected override void OnAttached(DependencyObject associatedObject)
        //{
        //    base.OnAttached();
        //    targetUpdatedHandler = new EventHandler<EventArgs>(TextBlockBindingUpdated);
        ////    Binding .AddTargetUpdatedHandler(this.AssociatedObject, targetUpdatedHandler);

        ////    // Run the initial behaviour logic
        ////    HighlightTextBlock(this.AssociatedObject);
        //}

        public void Detach()
        {
            var fe = AssociatedObject as FrameworkElement;
            if (fe != null)
            {
                
            }
            AssociatedObject = null;
        }

        #endregion

        #region Private Methods

        private void TextBlockBindingUpdated(object sender, EventArgs e)
        {
        //    var textBlock = e.TargetObject as TextBlock;
        //    if (textBlock == null)
        //        return;

        //    if (e.Property.Name == "Text")
        //        HighlightTextBlock(textBlock);
        }

        //private void HighlightTextBlock(TextBlock textBlock)
        //{
        //    foreach (var highlight in this.Highlights)
        //    {
        //        foreach (var range in FindAllPhrases(textBlock, highlight.Text))
        //        {
        //            if (highlight.Foreground != null)
        //                range.ApplyPropertyValue(TextElement.ForegroundProperty, highlight.Foreground);

        //            if (highlight.FontWeight != null)
        //                range.ApplyPropertyValue(TextElement.FontWeightProperty, highlight.FontWeight);
        //        }
        //    }
        //}

        //private List<TextRange> FindAllPhrases(TextBlock textBlock, string phrase)
        //{
        //    var result = new List<TextRange>();
        //    var position = textBlock.ContentStart;

        //    while (position != null)
        //    {
        //        var range = FindPhrase(position, phrase);
        //        if (range != null)
        //        {
        //            result.Add(range);
        //            position = range.End;
        //        }
        //        else
        //            position = null;
        //    }

        //    return result;
        //}

        // This method will search for a specified phrase (string) starting at a specified position.
        //private TextRange FindPhrase(TextPointer position, string phrase)
        //{
        //    while (position != null)
        //    {
        //        if (position.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
        //        {
        //            string textRun = position.GetTextInRun(LogicalDirection.Forward);

        //            // Find the starting index of any substring that matches "phrase".
        //            int indexInRun = textRun.IndexOf(phrase);
        //            if (indexInRun >= 0)
        //            {
        //                TextPointer start = position.GetPositionAtOffset(indexInRun);
        //                TextPointer end = start.GetPositionAtOffset(phrase.Length);
        //                return new TextRange(start, end);
        //            }
        //        }

        //        position = position.GetNextContextPosition(LogicalDirection.Forward);
        //    }

        //    // position will be null if "phrase" is not found.
        //    return null;
        //}

        #endregion
    }

    public class Highlight
    {
        public string Text { get; set; }
        public Brush Foreground { get; set; }
        public FontWeight FontWeight { get; set; }
    }
}

//<TextBlock Text = "Here is some text" >
//   < i:Interaction.Behaviors>
//      <behaviours:TextBlockHighlightBehaviour>
//         <behaviours:TextBlockHighlightBehaviour.Highlights>
//            <behaviours:Highlight Text = "some" Foreground="{StaticResource GreenBrush}" FontWeight="Bold" />
//            </behaviours:TextBlockHighlightBehaviour.Highlights>
//         </behaviours:TextBlockHighlightBehaviour>
//   </i:Interaction.Behaviors>
//</TextBlock>

//xmlns:i="http://schemas.microsoft.com/expression/2010/interactivity"
//xmlns:behaviours="clr-namespace:YourProject.Behviours"