﻿using LogicInterface;
using System.Reflection;

namespace BusinessLogic
{
    public class ReflectionPromotions : IReflectionPromotions
    {
        private readonly string basePath;

        public ReflectionPromotions()
        {
            basePath = AppDomain.CurrentDomain.BaseDirectory + "\\promotions";
        }

        public List<IPromotionable> ReturnListPromotions()
        {

            List<IPromotionable> listPromotions = new List<IPromotionable>();
            var paths = Directory.GetFiles(basePath, "*.dll");
            foreach (var path in paths)
            {

                Assembly currentAssembly = Assembly.LoadFile(path);
                if (currentAssembly != null)
                {
                    var objType = currentAssembly.GetTypes();
                    if (objType != null)
                    {
                        foreach (var type in objType)
                        {
                            if (typeof(IPromotionable).IsAssignableFrom(type))
                            {
                                IPromotionable promotion = (IPromotionable)Activator.CreateInstance(type);
                                listPromotions.Add(promotion);
                            }
                        }
                    }
                }
            }
            return listPromotions;
        }
    }
}