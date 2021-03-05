using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAppTest.Client.DragDropHandler
{
    /// <summary>
    /// Supplies information about an drag event that is being raised.
    /// </summary>
    public class DragEventArgs : MouseEventArgs
    {
        /// <summary>
        /// The data that underlies a drag-and-drop operation, known as the drag data store.
        /// See <see cref="DataTransfer"/>.
        /// </summary>
        public DataTransfer DataTransfer { get; set; }
    }
}
