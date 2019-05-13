using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace BottomsUpList
{
    public sealed partial class BottomUpList : UserControl
    {
        private bool _loadingMoreItems;

        public BottomUpList()
        {
            this.InitializeComponent();

            this.Loaded += BottomUpList_Loaded;
        }

        private void BottomUpList_Loaded(object sender, RoutedEventArgs e)
        {
            if (Scroller != null)
            {
                // Scroll to the end.
                //Scroller.ChangeView(null, Scroller.ExtentHeight - Scroller.ViewportHeight, null, true);
                Scroller.ScrollTo(0, Scroller.ExtentHeight - Scroller.ViewportHeight, new ScrollOptions(AnimationMode.Disabled));
            }
        }

        private bool LoadingMoreItems
        {
            get { return (bool)GetValue(LoadingMoreItemsProperty); }
            set { SetValue(LoadingMoreItemsProperty, value); }
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public object ItemsSource
        {
            get { return (object)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        private static readonly DependencyProperty LoadingMoreItemsProperty =
            DependencyProperty.Register(
                nameof(LoadingMoreItems),
                typeof(bool),
                typeof(BottomUpList),
                new PropertyMetadata(false));

        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register(
                nameof(ItemTemplate),
                typeof(DataTemplate),
                typeof(BottomUpList),
                new PropertyMetadata(null));

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                nameof(ItemsSource),
                typeof(object),
                typeof(BottomUpList),
                new PropertyMetadata(null));

        protected override Size ArrangeOverride(Size finalSize)
        {
            // Allow the panel to arrange first
            var result = base.ArrangeOverride(finalSize);

            //LoadMoreItems(20);

            return result;
        }

        //private void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        //{
        //    if (!e.IsIntermediate)
        //    {
        //        var itemsSource = ItemsSource as ISupportIncrementalLoading;
        //        var scroller = (Windows.UI.Xaml.Controls.ScrollViewer)sender;
        //        //var distanceToTop = scroller.ExtentHeight - (scroller.VerticalOffset + scroller.ViewportHeight);

        //        // trigger if within 2 viewports of the top
        //        if (scroller.VerticalOffset <= 2.0 * scroller.ViewportHeight
        //                && itemsSource.HasMoreItems && !LoadingMoreItems)
        //        {
        //            LoadMoreItems(15);
        //        }
        //    }
        //}

        private void ScrollViewer_ViewChanged(Microsoft.UI.Xaml.Controls.ScrollViewer scroller, object args)
        {
            var itemsSource = ItemsSource as ISupportIncrementalLoading;

            if (scroller.State == Microsoft.UI.Xaml.Controls.InteractionState.Idle)
            {
                // trigger if within 2 viewports of the top
                if (scroller.VerticalOffset <= 2.0 * scroller.ViewportHeight
                        && itemsSource.HasMoreItems && !LoadingMoreItems)
                {
                    LoadMoreItems(15);
                }
            }
        }

        private async void LoadMoreItems(int dataFetchSize)
        {
            var itemsSource = ItemsSource as ISupportIncrementalLoading;
            if (itemsSource == null) return;

            // show an indeterminate progress UI
            LoadingMoreItems = true;

            await itemsSource.LoadMoreItemsAsync(/*DataFetchSize*/ (uint)dataFetchSize);

            LoadingMoreItems = false;
        }

        //protected override void OnKeyDown(KeyRoutedEventArgs e)
        //{
        //    double verticalOffset = 0;
        //    switch (e.Key)
        //    {
        //        case Windows.System.VirtualKey.Up:
        //            verticalOffset = Scroller.VerticalOffset - Scroller.ViewportHeight / 8;
        //            break;
        //        case Windows.System.VirtualKey.Down:
        //            verticalOffset = Scroller.VerticalOffset + Scroller.ViewportHeight / 8;
        //            break;
        //        case Windows.System.VirtualKey.End:
        //            verticalOffset = Scroller.ExtentHeight - Scroller.ViewportHeight;
        //            break;
        //        case Windows.System.VirtualKey.Home:
        //            verticalOffset = 0;
        //            break;
        //        case Windows.System.VirtualKey.PageUp:
        //            verticalOffset = Scroller.VerticalOffset - Scroller.ViewportHeight;
        //            break;
        //        case Windows.System.VirtualKey.PageDown:
        //            verticalOffset = Scroller.VerticalOffset + Scroller.ViewportHeight;
        //            break;
        //        default:
        //            base.OnKeyDown(e);
        //            break;
        //    }

        //    if (verticalOffset >= 0)
        //    {
        //        //Scroller.ChangeView(null, verticalOffset, null);
        //        //Scroller.ScrollTo(Scroller.HorizontalOffset, verticalOffset);
        //    }
        //}
    }
}
