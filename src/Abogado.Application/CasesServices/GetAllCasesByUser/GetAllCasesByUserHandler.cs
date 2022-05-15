﻿using Abogado.Domain.Entities;
using Abogado.Domain.Ports;
using Ardalis.GuardClauses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abogado.Application.CasesServices.GetAllCasesByUser
{
    public class GetAllCasesByUserHandler : IRequestHandler<GetAllCasesByUserQuery, List<GetAllCasesByUserDTO>>
    {
        private readonly IRepository repository;

        private readonly IMapObject mapObject;

        public GetAllCasesByUserHandler(IRepository repository, IMapObject mapObject)
        {
            this.repository = repository;
            this.mapObject = mapObject;
        }

        public async Task<List<GetAllCasesByUserDTO>> Handle(GetAllCasesByUserQuery request, CancellationToken cancellationToken)
        {

            List<User> users = new();

            //Verificar que la peticion no se encuentre nula
            Guard.Against.Null(request, nameof(request));

            //Verificar que las 2 peticiones no esten vacias o nulas
            if (string.IsNullOrEmpty(request.Id) && string.IsNullOrEmpty(request.NameUser))
                throw new Exception("Los campos se encuentran vacios");

            //Obtener usuario 
            if ((!string.IsNullOrEmpty(request.Id)) && repository.Exists<User>(x => x.Id.ToString() == request.Id))
            {
                users.Add(await repository.GetNested<User>(x => x.Id.ToString() == request.Id, nameof(User.Cases)));
            }
            else if (!string.IsNullOrEmpty(request.NameUser))
            {
                users = await repository.GetAllNested<User>(x => x.Name.Contains(request.NameUser), nameof(User.Cases));
            }

            //Mapear y retornar entidad
            return mapObject.Map<List<User>, List<GetAllCasesByUserDTO>>(users);
        }
    }
}