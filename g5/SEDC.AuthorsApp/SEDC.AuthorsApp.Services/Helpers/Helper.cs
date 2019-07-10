using SEDC.AuthorsApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.AuthorsApp.Services
{
    public static class Helper
    {
        public static AwardType GetAwardEnum(string award)
        {
            AwardType awardEnum = (AwardType)Enum.Parse(typeof(AwardType), award);
            return awardEnum;
        }
    }
}
