using System.Collections.Generic;
using UnityEngine.UIElements;

namespace QuickEye.RequestWatcher
{
    internal partial class RequestConsole
    {
        private List<HDRequest> _source;

        public RequestConsole(VisualElement root)
        {
            AssignQueryResults(root);
            resultCol.makeCell = () => new StatusCodeCell();
            resultCol.bindCell = (element, i) =>
                ((StatusCodeCell)element).Setup(_source[i].lastResponse?.statusCode ?? 0);

            methodCol.makeCell = () => new MethodCell();
            methodCol.bindCell = (element, i) => ((MethodCell)element).Setup(_source[i].type.ToString());


            idCol.makeCell = () => new IdCell();
            idCol.bindCell = (element, i) => ((IdCell)element).Setup(_source[i].name);

            urlCol.makeCell = () => new UrlCell();
            urlCol.bindCell = (element, i) => ((UrlCell)element).Setup(_source[i].url);
            HttpClientLoggerEditorWrapper.ExchangeLogged += _ => requestList.Rebuild();
        }

        public void Setup(List<HDRequest> itemSource)
        {
            requestList.itemsSource = _source = itemSource;
            requestList.Rebuild();
        }
    }
}