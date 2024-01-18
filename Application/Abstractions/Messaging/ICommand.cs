﻿using Domain.ValueObjects;
using MediatR;
using System.Reflection;

namespace Application.Abstractions.Messaging
{
    public interface ICommand : IRequest<Result>
    {

    }
    public interface ICommand<TResponse> : IRequest<Result<TResponse>>
    {

    }
}
