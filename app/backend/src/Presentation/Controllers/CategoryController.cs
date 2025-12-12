using HouseholdBudget.Core.Application.Transactions.Commands;
using HouseholdBudget.Core.Application.Transactions.Interfaces;
using HouseholdBudget.Presentation.DTOs.Transactions;
using HouseholdBudget.Presentation.Filters;

using Microsoft.AspNetCore.Mvc;

namespace HouseholdBudget.Presentation.Controllers;

/// <summary>
/// カテゴリコントローラー
/// </summary>
[Route("category")]
[SessionAuthorize]
public class CategoryController : AppControllerBase
{
    private readonly IListCategoriesUseCase _listCategoriesUseCase;
    private readonly ICreateCategoryUseCase _createCategoryUseCase;
    private readonly IUpdateCategoryUseCase _updateCategoryUseCase;
    private readonly IDeleteCategoryUseCase _deleteCategoryUseCase;

    public CategoryController(
        IListCategoriesUseCase listCategoriesUseCase,
        ICreateCategoryUseCase createCategoryUseCase,
        IUpdateCategoryUseCase updateCategoryUseCase,
        IDeleteCategoryUseCase deleteCategoryUseCase)
    {
        _listCategoriesUseCase = listCategoriesUseCase;
        _createCategoryUseCase = createCategoryUseCase;
        _updateCategoryUseCase = updateCategoryUseCase;
        _deleteCategoryUseCase = deleteCategoryUseCase;
    }

    /// <summary>
    /// カテゴリ一覧取得
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ListCategories(CancellationToken cancellationToken)
    {
        var result = await _listCategoriesUseCase.ExecuteAsync(cancellationToken);

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }

        var response = result.Data!.Select(c => new CategoryResponse(c.CategoryId, c.CategoryName));
        return Ok(response);
    }

    /// <summary>
    /// カテゴリ登録
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateCategoryCommand(request.CategoryName);
        var result = await _createCategoryUseCase.ExecuteAsync(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }

        return StatusCode(StatusCodes.Status201Created);
    }

    /// <summary>
    /// カテゴリ更新
    /// </summary>
    [HttpPatch]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateCategoryCommand(request.OldCategoryName, request.NewCategoryName);
        var result = await _updateCategoryUseCase.ExecuteAsync(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }

        var response = new CategoryResponse(result.Data!.CategoryId, result.Data.CategoryName);
        return Ok(response);
    }

    /// <summary>
    /// カテゴリ削除
    /// </summary>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCategory([FromQuery] string categoryName, CancellationToken cancellationToken)
    {
        var command = new DeleteCategoryCommand(categoryName);
        var result = await _deleteCategoryUseCase.ExecuteAsync(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }

        return NoContent();
    }
}
