using ProjectSC.Models.Object.Appearance;
using System.Collections.Generic;

namespace ProjectSC.Models.Interface
{
    interface IAppearanceDataAccess
    {
        List<Themes> Retrieve();
    }
}
