using Squidex.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace FLDCVisitManager.CMSDataLayar.DTO
{
    public sealed class DynamicContent : SquidexEntityBase<DynamicData>
    {

    }
    public sealed class DynamicData : Content<DynamicData>
    {

    }

    public sealed class DynamicContentData : Dictionary<string, object>
    {

    }

    public sealed class DynamicContentDetails : Content<DynamicContentData>
    {

    }
}
