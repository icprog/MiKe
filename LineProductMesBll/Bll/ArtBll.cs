using System . Collections;
using System . Collections . Generic;
using System . Data;

namespace LineProductMesBll . Bll
{
    public class ArtBll
    {
        private readonly Dao.ArtDao dal=null;
        public ArtBll ( )
        {
            dal = new Dao . ArtDao ( );
        }


        /// <summary>
        /// 获取机台信息
        /// </summary>
        /// <returns></returns>
        public DataTable getTableView ( )
        {
            return dal . getTableView ( );
        }

        /// <summary>
        /// 获取车间
        /// </summary>
        /// <returns></returns>
        public DataTable getTableWork ( )
        {
            return dal . getTableWork ( );
        }

        /// <summary>
        /// 使用模具
        /// </summary>
        /// <returns></returns>
        public DataTable getTableMould ( )
        {
            return dal . getTableMould ( );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable getTableViewAll ( )
        {
            return dal . getTableViewAll ( );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable getTableViewMain ( string num )
        {
            return dal . getTableViewMain ( num );
        }

        /// <summary>
        /// 获取模具供应商
        /// </summary>
        /// <param name="piNum"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public DataTable getTableViewBody ( string piNum ,string num )
        {
            return dal . getTableViewBody ( piNum ,num );
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public bool Delete ( string num )
        {
            return dal . Delete ( num );
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CancellationBool ( LineProductMesEntityu . ArtEntity model )
        {
            return dal . CancellationBool ( model );
        }


        /// <summary>
        /// 获取商品信息
        /// </summary>
        /// <returns></returns>
        public DataTable getTableProInfo ( )
        {
            return dal . getTableProInfo ( );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="piNum"></param>
        /// <returns></returns>
        public LineProductMesEntityu . ArsEntity getModel ( string piNum )
        {
            return dal . getModel ( piNum );
        }


        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Save ( LineProductMesEntityu . ArsEntity model ,DataTable table,Hashtable haTable )
        {
            return dal . Save ( model ,table ,haTable );
        }

        /// <summary>
        /// 是否存在品号
        /// </summary>
        /// <param name="piNum"></param>
        /// <returns></returns>
        public bool Exists ( string piNum )
        {
            return dal . Exists ( piNum );
        }

        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="table"></param>
        /// <param name="idxList"></param>
        /// <returns></returns>
        public bool Edit ( LineProductMesEntityu . ArsEntity model ,DataTable table ,List<string> idxList ,Hashtable haTable ,List<string> idxListOne )
        {
            return dal . Edit ( model ,table ,idxList , haTable , idxListOne );
        }

        /// <summary>
        /// 获取工艺信息
        /// </summary>
        /// <returns></returns>
        public DataTable getTableArt ( string workId )
        {
            return dal . getTableArt ( workId );
        }

        /// <summary>
        /// 获取人员岗位
        /// </summary>
        /// <returns></returns>
        public DataTable getTablePost ( )
        {
            return dal . getTablePost ( );
        }

        /// <summary>
        /// 获取供应商信息
        /// </summary>
        /// <param name="piNum"></param>
        /// <returns></returns>
        public DataTable getTableProInfo ( string piNum )
        {
            return dal . getTableProInfo ( piNum );
        }

        /// <summary>
        /// 获取导出数据
        /// </summary>
        /// <param name="piList"></param>
        /// <returns></returns>
        public DataTable getTableExport ( List<string> piList )
        {
            return dal . getTableExport ( piList );
        }

    }
}
