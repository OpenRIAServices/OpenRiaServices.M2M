Breaking changes

05-15-2013 Breaking change in M2M API:
   The method ToLinkTable and its overloads have been renamed by two methods: ProjectObject1 and ProjectObject2. They project an entity type to the corresponding property 
   in a link table entity. The new methods are a little more descriptive then the previous names, but, more importantly, they are not ambiguous when used for self-referencing 
   m2m relations.
   
   This change requires the following small small change to your code:
   
   Given a ProductOrder m2m relation and an m2m view defined in the class Product:
	    
		public partial class Product
		{
			public ICollection<ProductOrder> Product_ProductOrderSet
			{
				get
				{
					return OrderSet.ToLinkTable<Product, Order, ProductOrder>(this);
				}
			}
		}

	you have to change the return statement to this:
		
		return OrderSet.ProjectObject1(this, x => x.Order_ProductOrderSet);

	Likewise, in the Order class:

		public partial class Order
		{
			public ICollection<ProductOrder> Order_ProductOrderSet
			{
				get
				{
					return ProductSet.ToLinkTable<Product, Order, ProductOrder>(this);
				}
			}
		}

	you have to change the return statement to this:
		return OrderSet.ProjectObject2(this, x => x.Product_ProductOrderSet);

05-15-2013 Added support for self-referencing m2m relations