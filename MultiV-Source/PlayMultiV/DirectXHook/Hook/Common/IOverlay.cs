using System.Collections.Generic;

namespace PlayMultiV.DirectXHook.Hook.Common
{
    internal interface IOverlay: IOverlayElement
    {
        List<IOverlayElement> Elements { get; set; }
    }
}
