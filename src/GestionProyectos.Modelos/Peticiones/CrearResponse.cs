﻿using ServiceStack;
using System.Collections.Generic;

namespace GestionProyectos.Modelos.Peticiones
{
    public class CrearResponse: IHasResponseStatus, IMeta
    {
        public virtual Dictionary<string, string> Meta { get; set; }
        public virtual ResponseStatus ResponseStatus { get; set; }
    }
}