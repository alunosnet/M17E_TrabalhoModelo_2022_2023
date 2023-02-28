using M17E_TrabalhoModelo_2022_2023.Data;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace M17E_TrabalhoModelo_2022_2023.Helper
{
    public static class Utils
    {
        public static string UserId(this HtmlHelper htmlHelper,
            System.Security.Principal.IPrincipal utilizador)
        {
            string iduser = "";

            using(var context=new M17E_TrabalhoModelo_2022_2023Context())
            {
                var consulta = context.Database.SqlQuery<int>("SELECT Id FROM utilizadors WHERE nome=@p0",
                    utilizador.Identity.Name);
                if (consulta.ToList().Count > 0)
                {
                    iduser = consulta.ToList()[0].ToString();
                }
            }

            return iduser;
        }
    }
}