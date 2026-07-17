namespace Application.Interfaces
{
    public interface IUseCase<TResult>
    {
        Task<TResult> ExecuteAsync();
    }
    public interface IUseCase<TInput, TResult>
    {
        Task<TResult> ExecuteAsync(TInput input);
    }
    public interface IUseCase<Tinput1, Tinput2, TResult>
    {
        Task<TResult> ExecuteAsync(Tinput1 input1, Tinput2 input2);
    }
}
