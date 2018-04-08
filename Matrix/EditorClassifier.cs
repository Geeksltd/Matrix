using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Reflection;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;

namespace Matrix
{
    class TestQuickInfoSource : IQuickInfoSource
    {
        private TestQuickInfoSourceProvider m_provider;
        private ITextBuffer m_subjectBuffer;

        public TestQuickInfoSource(TestQuickInfoSourceProvider provider, ITextBuffer subjectBuffer)
        {
            m_provider = provider;
            m_subjectBuffer = subjectBuffer;

        }

        /// Estefade baraye tashkhis kalame jari va jostejoye tozihat marboot be an
        public void AugmentQuickInfoSession(IQuickInfoSession session, IList<object> qiContent, out ITrackingSpan applicableToSpan)
        {
            try
            {
                Debug.WriteLine("AugmentQuickInfoSession Starts");
                SnapshotPoint ? subjectTriggerPoint = session.GetTriggerPoint(m_subjectBuffer.CurrentSnapshot);
                if (!subjectTriggerPoint.HasValue)
                {
                    applicableToSpan = null;
                    return;
                }

                ITextSnapshot currentSnapshot = subjectTriggerPoint.Value.Snapshot;
                SnapshotSpan querySpan = new SnapshotSpan(subjectTriggerPoint.Value, 0);

                ITextStructureNavigator navigator = m_provider.NavigatorService.GetTextStructureNavigator(m_subjectBuffer);
                TextExtent extent = navigator.GetExtentOfWord(subjectTriggerPoint.Value);
                string searchText = extent.Span.GetText();

                Debug.WriteLine("Befor Tools.CurrentSymbol");
                var symbol = Tools.CurrentSymbol(extent.Span);
                Debug.WriteLine("After Tools.CurrentSymbol");
                if (symbol != null)
                {
                    Debug.WriteLine("Befor new SamplePresenter");
                    SamplePresenter sp = new SamplePresenter(symbol);
                    Debug.WriteLine("After new SamplePresenter");
                    if (sp.FlagOk)
                    {
                        Debug.WriteLine("Befor new LinkButton");
                        LinkButton lnkBtn = new LinkButton();
                        lnkBtn.FormCaption = symbol.OriginalDefinition.ToDisplayString(Microsoft.CodeAnalysis.SymbolDisplayFormat.FullyQualifiedFormat);
                        Debug.WriteLine("After new LinkButton");
                        qiContent.Insert(0, lnkBtn);
                    }
                }

            }
            catch (Exception err)
            {
                StackTrace st = new StackTrace(err);
                Debug.WriteLine(st.ToString());
            }
            finally
            {
                applicableToSpan = null;
            }

        }

        private bool m_isDisposed;
        public void Dispose()
        {
            if (!m_isDisposed)
            {
                GC.SuppressFinalize(this);
                m_isDisposed = true;
            }
        }
    }

    [Export(typeof(IQuickInfoSourceProvider))]
    [Name("ToolTip QuickInfo Source")]
    [Order(After = "Default Quick Info Presenter")]
    [ContentType("CSharp")]
    internal class TestQuickInfoSourceProvider : IQuickInfoSourceProvider
    {
        [Import]
        internal ITextStructureNavigatorSelectorService NavigatorService { get; set; }

        [Import]
        internal ITextBufferFactoryService TextBufferFactoryService { get; set; }

        public IQuickInfoSource TryCreateQuickInfoSource(ITextBuffer textBuffer)
        {
            return new TestQuickInfoSource(this, textBuffer);
        }
    }

    internal class TestQuickInfoController : IIntellisenseController
    {
        private ITextView m_textView;
        private IList<ITextBuffer> m_subjectBuffers;
        private TestQuickInfoControllerProvider m_provider;
        private IQuickInfoSession m_session;

        internal TestQuickInfoController(ITextView textView, IList<ITextBuffer> subjectBuffers, TestQuickInfoControllerProvider provider)
        {
            m_textView = textView;
            m_subjectBuffers = subjectBuffers;
            m_provider = provider;

            m_textView.MouseHover += this.OnTextViewMouseHover;
        }

        private void OnTextViewMouseHover(object sender, MouseHoverEventArgs e)
        {
            //find the mouse position by mapping down to the subject buffer
            SnapshotPoint? point = m_textView.BufferGraph.MapDownToFirstMatch
                 (new SnapshotPoint(m_textView.TextSnapshot, e.Position),
                PointTrackingMode.Positive,
                snapshot => m_subjectBuffers.Contains(snapshot.TextBuffer),
                PositionAffinity.Predecessor);

            if (point != null)
            {
                ITrackingPoint triggerPoint = point.Value.Snapshot.CreateTrackingPoint(point.Value.Position,
                PointTrackingMode.Positive);

                if (!m_provider.QuickInfoBroker.IsQuickInfoActive(m_textView))
                {
                    m_session = m_provider.QuickInfoBroker.TriggerQuickInfo(m_textView, triggerPoint, true);
                }

            }
        }
        public void Detach(ITextView textView)
        {
            if (m_textView == textView)
            {
                m_textView.MouseHover -= this.OnTextViewMouseHover;
                m_textView = null;
            }
        }
        public void ConnectSubjectBuffer(ITextBuffer subjectBuffer)
        {
        }

        public void DisconnectSubjectBuffer(ITextBuffer subjectBuffer)
        {
        }
    }

    [Export(typeof(IIntellisenseControllerProvider))]
    [Name("ToolTip QuickInfo Controller")]
    [ContentType("CSharp")]
    internal class TestQuickInfoControllerProvider : IIntellisenseControllerProvider
    {
        [Import]
        internal IQuickInfoBroker QuickInfoBroker { get; set; }

        public IIntellisenseController TryCreateIntellisenseController(ITextView textView, IList<ITextBuffer> subjectBuffers)
        {
            return new TestQuickInfoController(textView, subjectBuffers, this);
        }
    }
}
