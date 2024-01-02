using Microsoft.EntityFrameworkCore;
using OnlineShopCart.API.Data;
using OnlineShopCart.API.Entities;
using OnlineShopCart.API.Repositories.Contracts;
using OnlineShopCart.Models.Dtos;



namespace OnlineShopCart.API.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ShopOnlineDBContext context;
        public ShoppingCartRepository(ShopOnlineDBContext context)
        {
            this.context = context;
        }
        public async Task<CartItem?> AddItem(CartItemToAddDto itemToAdd)
        {
            if (await ProductExists(itemToAdd.ProductId, itemToAdd.CartId))
            {
                return null;
            }

            //CartItem? item = await (from product in context.Products
            //                        where product.Id == itemToAdd.ProductId
            //                        select new CartItem
            //                        {
            //                            CartId = itemToAdd.CartId,
            //                            ProductId = itemToAdd.ProductId,
            //                            Quantity = itemToAdd.Quantity
            //                        }).SingleOrDefaultAsync();
            CartItem? item = await context.Products
                                          .Where(product => product.Id == itemToAdd.ProductId)
                                          .Select(product => new CartItem
                                          {
                                              CartId = itemToAdd.CartId,
                                              ProductId = itemToAdd.ProductId,
                                              Quantity = itemToAdd.Quantity
                                          })
                                          .SingleOrDefaultAsync();


            if (item != null)
            {
                Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<CartItem> result = await context.CartItems.AddAsync(item);
                _ = await context.SaveChangesAsync();
                return result.Entity;
            }
            else
            {
                return null;
            }
        }
        public async Task<CartItem?> UpdateQuantity(int id, CartItemQuantityUpdateDto item)
        {
            CartItem? itemToUpdate = await context.CartItems.FindAsync(id);
            if (itemToUpdate == null)
            {
                return null;
            }

            itemToUpdate.Quantity = item.Quantity;
            _ = await context.SaveChangesAsync();
            return itemToUpdate;
        }
        public async Task<CartItem?> DeleteItem(int id)
        {
            CartItem? item = await context.CartItems.FindAsync(id);
            if (item == null)
            {
                return null;
            }

            _ = context.CartItems.Remove(item);
            _ = await context.SaveChangesAsync();
            return item;
        }
        public async Task<CartItem?> GetItem(int id)
        {
            //return await(from cart in context.Carts
            //             join item in context.CartItems
            //             on cart.Id equals item.CartId
            //             where item.Id == id
            //             select new CartItem
            //             {
            //                 Id = item.Id,
            //                 CartId = item.CartId,
            //                 ProductId = item.ProductId,
            //                 Quantity = item.Quantity
            //             }).SingleOrDefaultAsync();
            return await context.Carts.Join(
                    context.CartItems,
            cart => cart.Id,
            item => item.CartId,
            (cart, item) => new { cart, item })
                                      .Where(joinResult => joinResult.item.Id == id)
                                      .Select(joinResult => new CartItem
                                      {
                                          Id = joinResult.item.Id,
                                          CartId = joinResult.item.CartId,
                                          ProductId = joinResult.item.ProductId,
                                          Quantity = joinResult.item.Quantity
                                      })
                                      .SingleOrDefaultAsync();
        }
        public async Task<IEnumerable<CartItem>> GetItems(int userId)
        {
            //return await (from cart in context.Carts
            //              join item in context.CartItems
            //              on cart.Id equals item.CartId
            //              where cart.UserId == userId
            //              select new CartItem
            //              {
            //                  Id = item.Id,
            //                  CartId = item.CartId,
            //                  ProductId = item.ProductId,
            //                  Quantity = item.Quantity
            //              }).ToListAsync();


            return await context.Carts.Join(
                context.CartItems,
        cart => cart.Id,
        item => item.CartId,
        (cart, item) => new { cart, item })
                        .Where(joinResult => joinResult.cart.UserId == userId)
                        .Select(joinResult => new CartItem
                        {
                            Id = joinResult.item.Id,
                            CartId = joinResult.item.CartId,
                            ProductId = joinResult.item.ProductId,
                            Quantity = joinResult.item.Quantity
                        })
                        .ToListAsync();
        }
        private async Task<bool> ProductExists(int productId, int cartId)
        {
            return await context.CartItems.AnyAsync(p => p.ProductId == productId && p.CartId == cartId);
        }
    }
}
