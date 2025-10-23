// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

namespace FxCore.Abstraction.Common.Models.Contracts;

/// <summary>
/// Defines a marker interface for all kinds of async request handlers.
/// </summary>
/// <typeparam name="TRequest">Type of the request.</typeparam>
public interface IAsyncRequestHandler<TRequest> : IAsyncHandler
    where TRequest : IRequest;
