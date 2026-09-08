namespace BookFlow.Core.Enums;

public enum DeleteResult
{
    Success,
    NotFound,
    HasDependencies,
    SqlProblem
}