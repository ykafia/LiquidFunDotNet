using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box2DNet.Common;

namespace Box2DNet.Collision.Shapes
{
    class ChainShape : Shape,ICloneable
    {
        public int m_count;
        public List<Vec2> m_vertices;
        public Vec2 m_prevVertex, m_nextVertex;
        public bool m_hasPrevVertex, m_hasNextVertex;

        public ChainShape()
        {
            _type = ShapeType.RopeShape;
            _radius = Settings.PolygonRadius;
        }
        public void CreateLoop(List<Vec2> vertices)
        {
            m_vertices = new List<Vec2>();
            for (int i = 1; i < vertices.Count; i++)
            {
                Vec2 v1 = vertices[i - 1];
                Vec2 v2 = vertices[i];
                if (MathB2.DistanceSquared(v1, v2) > Settings.LinearSlop * Settings.LinearSlop)
                {
                    throw new Exception("Two vertex are too close");
                }
            }
            m_vertices = vertices;
            m_hasPrevVertex = false;
            m_hasNextVertex = false;

        }
        public void CreateChain(List<Vec2> vertices)
        {
            m_vertices = new List<Vec2>();
            for (int i = 1; i < vertices.Count; i++)
            {
                Vec2 v1 = vertices[i - 1];
                Vec2 v2 = vertices[i];
                if(MathB2.DistanceSquared(v1,v2)>Settings.LinearSlop*Settings.LinearSlop)
                {
                    throw new Exception("Two vertex are too close");
                }
            }
            m_vertices = vertices;
            m_hasPrevVertex = false;
            m_hasNextVertex = false;
        }
        public void SetPrevVertex(Vec2 Prev)
        {
            m_prevVertex = Prev;
            m_hasPrevVertex = true; 
        }
        public void SetNextVertex(Vec2 next)
        {
            m_nextVertex = next;
            m_hasNextVertex = true;
        }
        

        public void ComputeAABB(out AABB aabb, XForm xf, int childIndex)
        {
            int i1 = childIndex;
            int i2 = childIndex + 1;
            if (i2 == m_count)
            {
                i2 = 0;
            }
            // TODO : Check if the override of the AABB function needed
            Vec2 v1 = MathB2.Mul(xf, m_vertices[i1]);
            Vec2 v2 = MathB2.Mul(xf, m_vertices[i2]);

            aabb.LowerBound = MathB2.Min(v1, v2);
            aabb.UpperBound = MathB2.Max(v1, v2);

        }

        public override void ComputeMass(out MassData massData, float density)
        {
            massData.Mass = 0.0f;
            massData.Center = new Vec2(0,0);
            massData.I = 0.0f;
        }

        public override float ComputeSubmergedArea(Vec2 normal, float offset, XForm xf, out Vec2 c)
        {
            throw new NotImplementedException();
        }

        public override float ComputeSweepRadius(Vec2 pivot)
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override int GetSupport(Vec2 d)
        {
            throw new NotImplementedException();
        }

        public override Vec2 GetSupportVertex(Vec2 d)
        {
            throw new NotImplementedException();
        }

        public override Vec2 GetVertex(int index)
        {
            throw new NotImplementedException();
        }

        public override bool TestPoint(XForm xf, Vec2 p)
        {
            throw new NotImplementedException();
        }

        public override SegmentCollide TestSegment(XForm xf, out float lambda, out Vec2 normal, Segment segment, float maxLambda)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }
        public int getChildrenCount()
        {
            return m_count - 1;
        }
        public void getChildEdge(EdgeShape edge,int index)
        {
            edge.SetType = ShapeType.EdgeShape;
            edge._radius = _radius;
            edge._v1 = m_vertices[index];
            edge._v2 = m_vertices[index + 1];
            if (index > 0)
            {
                edge.m_vertex0 = m_vertices[index - 1];
                edge.m_hasVertex0 = true;
            }
            else
            {
                edge.m_vertex0 = m_prevVertex;
                edge.m_hasVertex0 = m_hasPrevVertex;
            }
            if (index < m_count - 2)
            {
                edge.m_vertex3 = m_vertices[index + 2];
                edge.m_hasVertex3 = true;
            }
            else
            {
                edge.m_vertex3 = m_nextVertex;
                edge.m_hasVertex3 = m_hasNextVertex;
            }
        }

        public void ComputeDistance(XForm xf, Vec2 p, float distance, Vec2 normal, int childIndex)
        {
            EdgeShape edge = new EdgeShape();
            getChildEdge(edge, childIndex);
            edge.ComputeDistance(xf, p, distance, normal, 0);
        }
        
        public bool RayCast(RayCastOutput output, RayCastInput input,XForm xf,int childIndex)
        {
            EdgeShape edgeShape = new EdgeShape();

            int i1 = childIndex;
            int i2 = childIndex + 1;
            if (i2 == m_count)
            {
                i2 = 0;
            }

            edgeShape._v1 = m_vertices[i1];
            edgeShape._v2 = m_vertices[i2];

            return edgeShape.RayCast(output, input, xf, 0);
        }

        public override void ComputeAABB(out AABB aabb, XForm xf)
        {
            throw new NotImplementedException();
        }

        public int VertexCount { get { return this.m_vertices.Count; } }
    }
}
