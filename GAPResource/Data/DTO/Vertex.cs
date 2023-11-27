using System.Collections.Generic;

namespace GAPResource.Data
{
   
    public class MainVertexOrder
    {
        public MainVertex Vertex { get; private set; }
        public OrderDTO Order { get; private set; }
        public MainVertexOrder(MainVertex vertex, OrderDTO order)
        {
            Vertex = vertex;
            Order = order;  
        }
    }
        
        
        

    public class MainVertex
    {
        public long Vertex { get; private set; }
        public double PriceToParnet { get; private set; }

        public int Level { get; private set; } = 0;

        public MainVertex Parent { get; private set; }
        public HashSet<MainVertex> Childs { get; private set; } = new HashSet<MainVertex>();
        public MainVertex(long startVertex)
        {
            Vertex = startVertex;
        }


        public MainVertex(long startVertex, MainVertex parent, double priceToParent) : this(startVertex)
        {
            Vertex = startVertex;
            Parent = parent;
            PriceToParnet = priceToParent;
            Level = parent.Level + 1;
        }

        public bool UsiallyExists(long id)
        {
            if (Parent == null) return false;
            if (Parent.Vertex == id) return true;
            return Parent.UsiallyExists(id);
        }


        public MainVertex GetMainParent()
        {
            if (Parent == null) return this;
            return Parent.GetMainParent();
        }

        public bool OnTheOneWay(long firstEndPoint, long secondEndPoint)
        {
            if (firstEndPoint == secondEndPoint) return true;
            foreach (MainVertex vertex in Childs)
            {
                if (vertex.Vertex == firstEndPoint && vertex.OnTheOneWay(secondEndPoint))
                {
                    return true;
                }
                if (vertex.Vertex == secondEndPoint && vertex.OnTheOneWay(firstEndPoint))
                {
                    return true;
                }
            }
            return false;
        }

        private bool OnTheOneWay(long secondEndPoint)
        {
            foreach (MainVertex vertex in Childs)
            {
                if (vertex.Vertex == secondEndPoint || vertex.OnTheOneWay(secondEndPoint))
                {
                    return true;
                }
            }
            return false;
        }


        public MainVertexOrder GetPriceByWayAndPart(OrderDTO order, MainVertex mainVertex)
        {
            foreach (MainVertex vertex in mainVertex.Childs)
            {
                if (vertex.Vertex == order.RecipientCityId)
                {
                    return new MainVertexOrder(vertex, order);
                }
                var item = GetPriceByWayAndPart(order, vertex);
                if (item == null) continue;
                else return item;
            }
            return null;
        }


        public double GetAmount(long lastLevel)
        {
            double amount = 0;
            var parent = this;
            while(parent.Level != lastLevel)
            {
                amount += parent.PriceToParnet;
                parent = parent.Parent;
            }
            return amount;
        }


        public List<MainVertexOrder> GetPriceByWayAndPart(List<OrderDTO> orders)
        {
            List<MainVertexOrder> items = new List<MainVertexOrder>();
            foreach (var order in orders)
            {
                items.Add(GetPriceByWayAndPart(order, this));
            }
            return items.OrderBy(p=>p.Vertex.Level).ToList();
        }



    }


}
