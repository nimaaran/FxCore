// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Models.Contracts;
using FxCore.Abstraction.Persistence.Specifications.Contracts;
using System.Linq.Expressions;

namespace FxCore.Abstraction.Persistence.Specifications;

/// <summary>
/// Represents a criterion that is used to define a query.
/// </summary>
/// <typeparam name="TModel">The type of the data model.</typeparam>
public class Criterion<TModel> : ICriterion<TModel>
    where TModel : class, IDataModel
{
    private readonly List<(CriteriaOperators, ICriterion<TModel>)> clauses = [];
    private Expression<Func<TModel, bool>> condition = (e) => true;

    /// <inheritdoc/>
    public ICriterion<TModel> Or(ICriterion<TModel> criterion)
    {
        this.clauses.Add(new(CriteriaOperators.OR, criterion));
        return this;
    }

    /// <inheritdoc/>
    public ICriterion<TModel> And(ICriterion<TModel> criterion)
    {
        this.clauses.Add(new(CriteriaOperators.AND, criterion));
        return this;
    }

    /// <inheritdoc/>
    public ICriterion<TModel> Set(Expression<Func<TModel, bool>> condition)
    {
        this.condition = condition;
        return this;
    }

    /// <inheritdoc/>
    public Expression<Func<TModel, bool>> Export()
    {
        Expression<Func<TModel, bool>> And(
            Expression<Func<TModel, bool>> left,
            Expression<Func<TModel, bool>> right)
        {
            var invokedRight = Expression.Invoke(right, left.Parameters);
            var body = Expression.AndAlso(left.Body, invokedRight);
            return Expression.Lambda<Func<TModel, bool>>(body, left.Parameters);
        }

        Expression<Func<TModel, bool>> Or(
            Expression<Func<TModel, bool>> left,
            Expression<Func<TModel, bool>> right)
        {
            var invokedRight = Expression.Invoke(right, left.Parameters);
            var body = Expression.OrElse(left.Body, invokedRight);
            return Expression.Lambda<Func<TModel, bool>>(body, left.Parameters);
        }

        if (this.clauses.Count > 0)
        {
            foreach (var (@operator, clause) in this.clauses)
            {
                var evaluatedExpression = clause.Export();

                this.condition = @operator == CriteriaOperators.AND ?
                    And(this.condition, evaluatedExpression) :
                    Or(this.condition, evaluatedExpression);
            }
        }

        return this.condition;
    }
}
