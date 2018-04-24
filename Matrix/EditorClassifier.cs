using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Utilities;
using Matrix.Logic;
using Matrix.Infrustructure;

namespace Matrix
{
    [EscapeGCop("This class must be consistant as possible.")]
    class TestQuickInfoSource : IQuickInfoSource
    {
        TestQuickInfoSourceProvider m_provider;
        ITextBuffer m_subjectBuffer;

        public TestQuickInfoSource(TestQuickInfoSourceProvider provider, ITextBuffer subjectBuffer)
        {
            m_provider = provider;
            m_subjectBuffer = subjectBuffer;
        }

        public void AugmentQuickInfoSession(IQuickInfoSession session, IList<object> qiContent, out ITrackingSpan applicableToSpan)
        {
            try
            {
                Debug.WriteLine("AugmentQuickInfoSession Starts");
                var subjectTriggerPoint = session.GetTriggerPoint(m_subjectBuffer.CurrentSnapshot);
                if (!subjectTriggerPoint.HasValue)
                {
                    applicableToSpan = null;
                    return;
                }
                var navigator = m_provider.NavigatorService.GetTextStructureNavigator(m_subjectBuffer);
                var extent = navigator.GetExtentOfWord(subjectTriggerPoint.Value);
                var symbol = CodeAnalysis.GetCurrentSymbol(extent.Span);
                if (symbol != null)
                {
                    Current.Symbol = symbol;
                    var lnkBtn = new LinkButton
                    {
                        FormCaption = symbol.OriginalDefinition.ToDisplayString(Microsoft.CodeAnalysis.SymbolDisplayFormat.FullyQualifiedFormat)
                    };
                    qiContent.Insert(0, lnkBtn);
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.ToString() + new StackTrace(err).ToString());
            }
            finally
            {
                applicableToSpan = null;
            }
        }

        bool m_isDisposed;
        public void Dispose()
        {
            if (!m_isDisposed)
            {
                GC.SuppressFinalize(this);
                m_isDisposed = true;
            }
        }
    }

    [EscapeGCop("This class must be consistant as possible.")]
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

    [EscapeGCop("This class must be consistant as possible.")]
    internal class TestQuickInfoController : IIntellisenseController
    {
        ITextView m_textView;
        IList<ITextBuffer> m_subjectBuffers;
        TestQuickInfoControllerProvider m_provider;
        IQuickInfoSession m_session;

        internal TestQuickInfoController(ITextView textView, IList<ITextBuffer> subjectBuffers, TestQuickInfoControllerProvider provider)
        {
            m_textView = textView;
            m_subjectBuffers = subjectBuffers;
            m_provider = provider;

            m_textView.MouseHover += OnTextViewMouseHover;
        }

        void OnTextViewMouseHover(object sender, MouseHoverEventArgs e)
        {
            // find the mouse position by mapping down to the subject buffer
            var point = m_textView.BufferGraph.MapDownToFirstMatch
                 (new SnapshotPoint(m_textView.TextSnapshot, e.Position),
                PointTrackingMode.Positive,
                snapshot => m_subjectBuffers.Contains(snapshot.TextBuffer),
                PositionAffinity.Predecessor);

            if (point != null)
            {
                var triggerPoint = point.Value.Snapshot.CreateTrackingPoint(point.Value.Position,
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
                m_textView.MouseHover -= OnTextViewMouseHover;
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

    [EscapeGCop("This class must be consistant as possible.")]
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