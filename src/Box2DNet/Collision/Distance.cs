using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box2DNet.Collision.Shapes;
using Box2DNet.Common;

namespace Box2DNet.Collision
{
    class Distance
    {
        struct DistanceProxy
        {

            Vec2[] m_buffer;
            Vec2[] m_vertices;
            int m_count;
            float m_radius;
            
            /// Initialize the proxy using the given shape. The shape
            /// must remain in scope while the proxy is in use.
            public void Set(Shape shape, int index)
            {
                switch (shape.Type)
                {
                    case ShapeType.CircleShape:
                        {
                            CircleShape circle = (CircleShape)shape;
                            m_vertices = new Vec2[] { circle.Position };
                            m_count = 1;
                            m_radius = circle._radius;
                        }
                        break;

                    case ShapeType.PolygonShape:
                        {
                            PolygonShape polygon = (PolygonShape)shape;
                            m_vertices = polygon.Vertices;
                            m_count = polygon.VertexCount;
                            m_radius = polygon._radius;
                        }
                        break;

                    case ShapeType.ChainShape:
                        {
                            ChainShape chain = (ChainShape)shape;
                            if (!(0 <= index && index < chain.VertexCount))
                                break;

                            m_buffer[0] = chain.m_vertices[index];
                            if (index + 1 < chain.VertexCount)
                            {
                                m_buffer[1] = chain.m_vertices[index + 1];
                            }
                            else
                            {
                                m_buffer[1] = chain.m_vertices[0];
                            }

                            m_vertices = m_buffer;
                            m_count = 2;
                            m_radius = chain._radius;
                        }
                        break;

                    case ShapeType.EdgeShape:
                        {
                            EdgeShape edge = (EdgeShape)shape;
                            m_vertices = edge.Vertices;
                            m_count = 2;
                            m_radius = edge._radius;
                        }
                        break;

                    default:
                        break;
                }
            }
            public Vec2 GetVertex(int id)
            {
                return m_vertices[id];
            }


        }
        struct SimplexVertex
        {
            public Vec2 wA;      // support point in proxyA
            public Vec2 wB;      // support point in proxyB
            public Vec2 w;       // wB - wA
            public float a;      // barycentric coordinate for closest point
            public int indexA;   // wA index
            public int indexB;   // wB index
        }

        
        struct Simplex
            {
            SimplexVertex[] m_v1, m_v2, m_v3;
            int m_count;
            private float m_radius;
            private Vec2[] m_buffer;
            private Vec2[] m_vertices;

            void ReadCache( SimplexCache cache, DistanceProxy proxyA, XForm transformA, DistanceProxy proxyB, XForm transformB)
	            {
                   

                // Copy data from cache.
                m_count = cache.count;
		        SimplexVertex[] vertices = m_v1;
		        for (int i = 0; i<m_count; ++i)
		        {


                    SimplexVertex v = vertices[i];
                    v.indexA = cache.indexA[i];
			        v.indexB = cache.indexB[i];
			        Vec2 wALocal = proxyA.GetVertex(v.indexA);
                    Vec2 wBLocal = proxyB.GetVertex(v.indexB);
                    v.wA = MathB2.Mul(transformA, wALocal);
                    v.wB = MathB2.Mul(transformB, wBLocal);
                    v.w = v.wB - v.wA;
			        v.a = 0.0f;
		        }

		// Compute the new simplex metric, if it is substantially different than
		// old metric then flush the simplex.
		if (m_count > 1)
		{
			float metric1 = cache.metric;
            float metric2 = GetMetric();
			if (metric2< 0.5f * metric1 || 2.0f * metric1<metric2 || metric2<Settings.FLT_EPSILON)
			{
				// Reset the simplex.
				m_count = 0;
			}
    }

		// If the cache is empty or invalid ...
		if (m_count == 0)
		{
			SimplexVertex v = vertices[0];
            v.indexA = 0;
			v.indexB = 0;
			Vec2 wALocal = proxyA.GetVertex(0);
            Vec2 wBLocal = proxyB.GetVertex(0);
            v.wA = MathB2.Mul(transformA, wALocal);
            v.wB = MathB2.Mul(transformB, wBLocal);
            v.w = v.wB - v.wA;
			v.a = 1.0f;
			m_count = 1;
		}
	}
            
            public float GetMetric()
            {
                
                switch (m_count)
                {
                    case 0:
                        return 0.0f;

                    case 1:
                        return 0.0f;

                    case 2:
                        return DistanceB2(m_v1.w, m_v2.w);

                    case 3:
                        return Cross(m_v2.w- m_v1.w, m_v3.w - m_v1.w);

                    default:
                        return 0.0f;
                }
            }

            private float Cross(object p1, object p2)
            {
                throw new NotImplementedException();
            }


            /// Get the supporting vertex index in the given direction.
            int GetSupport(Vec2 d)
            {
                return 0;
            }
              

            public DistanceProxy(Vec2[] m_buffer, Vec2[] m_vertices, int m_count, float m_radius)
            {
                this.m_buffer = m_buffer;
                this.m_vertices = m_vertices;
                this.m_count = m_count;
                this.m_radius = m_radius;
            }

            /// Get the supporting vertex in the given direction.
            Vec2 GetSupportVertex(Vec2 d)
            {
                return new Vec2(2, 2);
            }
            

            /// Get the vertex count.
            int GetVertexCount()
            {
                return 0;
            }

            /// Get a vertex by index. Used by b2Distance.
            Vec2 GetVertex(int index)
            {
                
            }   
            

            
        }
        */
        struct SimplexCache
        {
            public float metric;     ///< length or area
            public int count;
            public int[] indexA;    ///< vertices on shape A
            public int[] indexB;    ///< vertices on shape B
        }
        struct DistanceInput
        {
            DistanceProxy proxyA;
            DistanceProxy proxyB;
            XForm transformA;
            XForm transformB;
            bool useRadii;
        };

        /// Output for b2Distance.
        struct DistanceOutput
        {
            Vec2 pointA;      ///< closest point on shapeA
            Vec2 pointB;      ///< closest point on shapeB
            float distance;
            int iterations;   ///< number of GJK iterations used
        }
        void DistanceB2(DistanceOutput output,SimplexCache cache, DistanceInput input)
        {

        }



    }
}
