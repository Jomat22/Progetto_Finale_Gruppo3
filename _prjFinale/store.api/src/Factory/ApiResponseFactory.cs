using Microsoft.AspNetCore.Mvc.ModelBinding;
using store.api.src.Common;
namespace store.api.src.Factory;

public static class ApiResponseFactory
{
    public static ApiResponse_Success<T> Success<T>(T data)
    {
        return new ApiResponse_Success<T>()
        {
            IsSuccess = true,
            StatusCode = 200,
            Message = "Operazione completata con successo.",
            Data = data,
        };
    }

    public static ApiResponse_Success<T> SuccessNoContent<T>(T data)
    {
        return new ApiResponse_Success<T>()
        {
            IsSuccess = true,
            StatusCode = 200,
            Message = "Operazione completata. Nessun record trovato.",
            Data = data,
        };
    }

    public static ApiResponse_Success<T> SuccessNoChanges<T>(T data)
    {
        return new ApiResponse_Success<T>()
        {
            IsSuccess = true,
            StatusCode = 200,
            Message = "Operazione completata. Nessuna scrittura effettuta.",
            Data = data,
        };
    }

    public static ApiResponse_Error BadInput_ModelState(ModelStateDictionary modelState) 
    {
        return new ApiResponse_Error
        {
            IsSuccess = false,
            StatusCode = 400,
            Message = "Operazione fallita. I dati forniti non sono corretti.",
            Error = modelState.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value?.Errors?.Select(e => e.ErrorMessage).ToArray() ?? []
            )
        };
    }

    public static ApiResponse_Error NotFound()
    {
        return new ApiResponse_Error
        {
            IsSuccess = false,
            StatusCode = 404,
            Message = "Operazione fallita. Record inesistente.",
            Error = [],
        };
    }

    public static ApiResponse_Error Conflict()
    {
        return new ApiResponse_Error
        {
            IsSuccess = false,
            StatusCode = 409,
            Message = "Operazione fallita. Record già esistente.",
            Error = [],
        };
    }

    public static ApiResponse_Error Unauthorized()
    {
        return new ApiResponse_Error
        {
            IsSuccess = false,
            StatusCode = 401,
            Message = "Operazione fallita. Credenziali non valide.",
            Error = [],
        };
    }

    public static ApiResponse_Error InternalServerError()
    {
        return new ApiResponse_Error()
        {
            IsSuccess = false,
            StatusCode = 500,
            Message = "Operazione fallita. Errore durante il processamento della richiesta.",
            Error = [],
        };
    }
}
