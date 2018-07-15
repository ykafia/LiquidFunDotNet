using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box2DNet.Common;

namespace Box2DNet.Collision.Shapes
{
    class RopeShape : Shape,ICloneable
    {
        public int m_count;
        public List<Vec2> m_vertices;
        public Vec2 m_prevVertex, m_nextVertex;
        public bool m_hasPrevVertex, m_hasNextVertex;

        public RopeShape()
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
        

        public override void ComputeAABB(out AABB aabb, XForm xf)
        {
            throw new NotImplementedException();
            
        }

        public override void ComputeMass(out MassData massData, float density)
        {
            throw new NotImplementedException();
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

        public void ComputeDistance(MathB2.Transform xf, const b2Vec2& p, float32* distance, b2Vec2* normal, int32 childIndex)

        public int VertexCount { get { return this.m_vertices.Count; } }
    }
}
