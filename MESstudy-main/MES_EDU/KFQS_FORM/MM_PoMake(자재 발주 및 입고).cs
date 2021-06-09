﻿using DC00_assm;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KFQS_Form
{
    public partial class MM_PoMake : DC00_WinForm.BaseMDIChildForm
    {
        //그리드 세팅 도와주는 함수 클래스
        UltraGridUtil _gridUtil = new UltraGridUtil();

        //공장 변수 입력
        //private sPlantCode = LoginInfo.
        public MM_PoMake()
        {
            InitializeComponent();
        }

        private void MM_PoMake_Load(object sender, EventArgs e)
        {
            //그리드를 세팅한다.
            try
            {
                _gridUtil.InitializeGrid(this.grid1, false, true, false, "", false);
                _gridUtil.InitColumnUltraGrid(grid1, "PLANTCODE",        "공장", true,                    GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, true);
                _gridUtil.InitColumnUltraGrid(grid1, "PONO",            "발주번호", true,                    GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, true);
                _gridUtil.InitColumnUltraGrid(grid1, "ITEMCODE",      "발주품목코드", true,                    GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, true);
                _gridUtil.InitColumnUltraGrid(grid1, "ITEMNAME",       "발주품목명", true,                    GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, true);
                _gridUtil.InitColumnUltraGrid(grid1, "PODATE",         "발주일자", true,                    GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, true);
                _gridUtil.InitColumnUltraGrid(grid1, "POQTY",         "발주수량", true,                    GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, true);
                _gridUtil.InitColumnUltraGrid(grid1, "UNITCODE",       "단위", true,                    GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, true);
                _gridUtil.InitColumnUltraGrid(grid1, "CUSTCODE",       "거래처", true,                    GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, true);
                _gridUtil.InitColumnUltraGrid(grid1, "CHK",            "입고선택", true,                    GridColDataType_emu.CheckBox, 130, 130, Infragistics.Win.HAlign.Left, true, true);
                _gridUtil.InitColumnUltraGrid(grid1, "INQTY",          "입고수량", true,                   GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, true);
                _gridUtil.InitColumnUltraGrid(grid1, "LOTNO",          "LOT번호", true,                   GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _gridUtil.InitColumnUltraGrid(grid1, "INDATE",            "입고일자", true,                   GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _gridUtil.InitColumnUltraGrid(grid1, "INWORKER",        "입고자", true,                   GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _gridUtil.InitColumnUltraGrid(grid1, "MAKEDATE",          "등록일시", true,                   GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _gridUtil.InitColumnUltraGrid(grid1, "MAKER",          "등록자", true,                   GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _gridUtil.InitColumnUltraGrid(grid1, "EDITDATE",          "등록일시", true,                   GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _gridUtil.SetInitUltraGridBind(grid1);

                //콤보 박스 셋팅
                Common _Common = new Common();
                DataTable dtTemp = new DataTable();
                // PLANTCODE 기준정보 가져와서 데이터 테이블에 추가
                dtTemp = _Common.Standard_CODE("PLANTCODE");

                // 데이터 테이블에 있는 데이터를 해당 콤보박스에 추가.
                // CODE_ID 벨류를 CODE_NAME로 디스 플레이
                Common.FillComboboxMaster(this.cmb_plant, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", dtTemp, "CODE_ID", "CODE_NAME");

                dtTemp = _Common.Standard_CODE("UNITCODE");
                
                UltraGridUtil.SetComboUltraGrid(this.grid1, "UNITCODE", dtTemp, "CODE_ID", "CODE_NAME");
                // 데이터 테이블에 표현할 데이터 가져오기 


                
                dtTemp = _Common.GET_TB_CUSTMATTER_CODE("");
               // 조회에 있는 콤보박스 컨트롤에 데이터 등록 
                
                // 그리드에 있는 해당 컬럼에 콤보 박스 형태로 데이터 등록 
                Common.FillComboboxMaster(this.cmb_CUST_PLACE, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "CUSTCODE", dtTemp, "CODE_ID", "CODE_NAME");

                dtTemp = _Common.GET_ItemCodeFERT_Code("ROH");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "ITEMCODE", dtTemp, "CODE_ID", "CODE_NAME");


                cmb_plant.Value = LoginInfo.PlantCode;
                DTPSTART.Value = string.Format("{0:yyyy-MM-01}", DateTime.Now);





            }
            catch (Exception ex)
            {
                ShowDialog(ex.Message, DC00_WinForm.DialogForm.DialogType.OK);

            }
        }
        public override void DoInquire()
        {
            base.DoInquire();
            DBHelper helper = new DBHelper(false);
            try
            {
                string sPlantcode = cmb_plant.Value.ToString();
                string sPono = TXT_poNum.Text.ToString();
                string sCustCode = cmb_CUST_PLACE.Value.ToString();
                string sStart = string.Format("{0:yyyy-MM-dd}", DTPSTART.Value);
                string sEnd = string.Format("{0:yyyy-MM-dd}", DTPEND.Value);
                //string sUseFlag = cmb_UseFlag.Value.ToString();

                DataTable dttemp = new DataTable();
                dttemp = helper.FillTable("04MM_PoMake_S1", CommandType.StoredProcedure,
                    helper.CreateParameter("PLANTCODE", sPlantcode, DbType.String, ParameterDirection.Input),
                helper.CreateParameter("CUSTCODE", sCustCode, DbType.String, ParameterDirection.Input),
                helper.CreateParameter("PONO", sPono, DbType.String, ParameterDirection.Input),
                helper.CreateParameter("STARTDATE", sStart, DbType.String, ParameterDirection.Input),
                helper.CreateParameter("ENDDATE", sEnd, DbType.String, ParameterDirection.Input));
                this.ClosePrgForm();
                if (dttemp.Rows.Count > 0)
                {
                    grid1.DataSource = dttemp;
                    grid1.DataBinds(dttemp);

                }
                else
                {
                    _gridUtil.Grid_Clear(grid1);
                    ShowDialog("조회할 데이터가 없습니다", DC00_WinForm.DialogForm.DialogType.OK);

                }
            }
            catch (Exception ex)
            {

                ShowDialog(ex.Message, DC00_WinForm.DialogForm.DialogType.OK);
            }
            finally
            {
                helper.Close();
            }

        }
            public override void DoNew()
        {
            base.DoNew();
            this.grid1.InsertRow();
            this.grid1.ActiveRow.Cells["PLANTCODE"].Value   = LoginInfo.PlantCode;
            grid1.ActiveRow.Cells["PONO"].Activation        = Activation.NoEdit;
            grid1.ActiveRow.Cells["CHK"].Activation         = Activation.NoEdit;
            grid1.ActiveRow.Cells["LOTNO"].Activation       = Activation.NoEdit;
            grid1.ActiveRow.Cells["INDATE"].Activation      = Activation.NoEdit;
            grid1.ActiveRow.Cells["INWORKER"].Activation    = Activation.NoEdit;
            grid1.ActiveRow.Cells["MAKER"].Activation       = Activation.NoEdit;
            grid1.ActiveRow.Cells["MAKEDATE"].Activation    = Activation.NoEdit;
            grid1.ActiveRow.Cells["EDITDATE"].Activation    = Activation.NoEdit;
            grid1.ActiveRow.Cells["EDITOR"].Activation      = Activation.NoEdit;



            

        }
        public override void DoDelete()
        {
            base.DoDelete();
            if (Convert.ToString(this.grid1.ActiveRow.Cells["CHK"].Value) == "1")
            {
                ShowDialog("입고된 발주 내역은 삭제 불가능.", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }
            this.grid1.DeleteRow();

        }
        public override void DoSave()
        {
            base.DoSave();
            DataTable dttemp = new DataTable();
            dttemp = grid1.chkChange();
            if (dttemp == null) { return; }
            DBHelper helper = new DBHelper("", true);
            try
            {
                //해당 내역을 저장하시겠습니까?
                if (ShowDialog("해당 사항을 저장 하시겠습니까? ",DC00_WinForm.DialogForm.DialogType.YESNO)== System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
                {

                }
                foreach (DataRow drrow in dttemp.Rows)
                {
                    
                    switch (drrow.RowState)
                    {
                        case DataRowState.Deleted:
                            drrow.RejectChanges();
                            helper.ExecuteNoneQuery("04MM_PoMake_D1", CommandType.StoredProcedure,
                                helper.CreateParameter("PLANTCODE", Convert.ToString(drrow["PLANTCODE"]), DbType.String, ParameterDirection.Input),
                                helper.CreateParameter("PONO", Convert.ToString(drrow["PONO"]), DbType.String, ParameterDirection.Input)

                                );
                                
                              

                            break;
                        case DataRowState.Added:
                            string sErrorMsg = string.Empty;
                            
                            if (Convert.ToString(drrow["ITEMCODE"]) == "")
                            {
                                sErrorMsg += "품목";
                            }
                            if (Convert.ToString(drrow["POQTY"]) == "")
                            {
                                sErrorMsg += "발주 수량";
                            }
                            if (Convert.ToString(drrow["ITEMCODE"]) == "")
                            {
                                sErrorMsg += "거래처";
                            }
                            if (sErrorMsg != "")
                            {
                                this.ClosePrgForm();
                                ShowDialog(sErrorMsg + "를 입력하지 않았습니다", DC00_WinForm.DialogForm.DialogType.OK);
                                helper.Rollback();
                                return;

                            }
                            helper.ExecuteNoneQuery("04MM_PoMake_I1"
                                                    , CommandType.StoredProcedure
                                                    , helper.CreateParameter("PLANTCODE", Convert.ToString(drrow["PLANTCODE"]), DbType.String, ParameterDirection.Input)
                                                    , helper.CreateParameter("ITEMCODE", Convert.ToString(drrow["ITEMCODE"]), DbType.String, ParameterDirection.Input)
                                                    , helper.CreateParameter("POQTY", Convert.ToString(drrow["POQTY"]), DbType.String, ParameterDirection.Input)
                                                    , helper.CreateParameter("UNITCODE", Convert.ToString(drrow["UNITCODE"]), DbType.String, ParameterDirection.Input)
                                                    , helper.CreateParameter("CUSTCODE", Convert.ToString(drrow["CUSTCODE"]), DbType.String, ParameterDirection.Input)
                                                    , helper.CreateParameter("MAKER", LoginInfo.UserID, DbType.String, ParameterDirection.Input)
                                                    

                                                    );
                            break;
                        case DataRowState.Modified:
                            helper.ExecuteNoneQuery("04MM_PoMake_U1"
                                                    , CommandType.StoredProcedure
                                                    , helper.CreateParameter("PLANTCODE", Convert.ToString(drrow["PLANTCODE"]), DbType.String, ParameterDirection.Input)
                                                    
                                                    , helper.CreateParameter("INQTY", Convert.ToString(drrow["INQTY"]), DbType.String, ParameterDirection.Input)
                                                    , helper.CreateParameter("PONO", Convert.ToString(drrow["PONO"]), DbType.String, ParameterDirection.Input)

                                                    , helper.CreateParameter("EDITOR", LoginInfo.UserID, DbType.String, ParameterDirection.Input)

                                                    );
                            break;
                        
                    }
                }
                if (helper.RSCODE == "S")
                {
                    string s = helper.RSMSG;
                    helper.Commit();
                    this.ShowDialog("등록 완료!");
                    DoInquire();

                }
                

            }
            catch (Exception ex)
            {

                ShowDialog($"{ex}");
            }
            finally
            {
                helper.Close();
            }
        }
    }
        //MES의 특징적인 4M 중... LOT는 상당히 중요한 개념입니다. 
        //LOT별 흐름을 확인하는 것이다. 하나의 단위
        //발주내역(발주테이블 WITH 입고테이블)->

}

