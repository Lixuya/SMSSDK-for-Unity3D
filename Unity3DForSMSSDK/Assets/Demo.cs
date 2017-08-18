﻿using UnityEngine;
using System.Collections;
using System;
using cn.SMSSDK.Unity; 

namespace cn.SMSSDK.Unity
{
	public class Demo : MonoBehaviour,SMSSDKHandler {

		// Use this for initialization
		public GUISkin demoSkin;
		public SMSSDK smssdk;
		public UserInfo userInfo;

		//please add your phone number
		private string phone = "";
		private string zone = "86";
		private string code = "";
		private string result = null;


		void Start () 
		{
			Debug.Log("[SMSSDK]Demo  ===>>>  Start" );
			smssdk = gameObject.GetComponent<SMSSDK>();
			smssdk.init("moba6b6c6d6","b89d2427a3bc7ad1aea1e1e8c1d36bf3",true);
			userInfo = new UserInfo ();
			smssdk.setHandler(this);
		}

		// Update is called once per frame
		void Update () 
		{
			if (Input.GetKeyDown(KeyCode.Escape)) {
				Application.Quit();
			}
		}


		void OnGUI ()
		{
			GUI.skin = demoSkin;

			float scale = 1.0f;
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				scale = Screen.width / 320;
			}
				
			float btnWidth = 200 * scale;
			float btnHeight = 30 * scale;
			float btnTop = 50 * scale;
			GUI.skin.button.fontSize = Convert.ToInt32(14 * scale);
			GUI.skin.label.fontSize = Convert.ToInt32 (14 * scale);
			GUI.skin.label.alignment = TextAnchor.MiddleCenter;
			GUI.skin.textField.fontSize = Convert.ToInt32 (14 * scale);
			GUI.skin.textField.alignment = TextAnchor.MiddleCenter;

			GUI.Label (new Rect ((Screen.width - btnWidth) / 2, btnTop+5, 100, btnHeight), "手机号");

			phone = GUI.TextField(new Rect((Screen.width - btnWidth) / 2 + 100, btnTop, btnWidth - 100, btnHeight), phone);

			btnTop += btnHeight + 10 * scale;

			GUI.Label (new Rect ((Screen.width - btnWidth) / 2, btnTop+5, 100, btnHeight), "区号");

			zone = GUI.TextField(new Rect((Screen.width - btnWidth) / 2 + 100, btnTop, btnWidth - 100, btnHeight), zone);

			btnTop += btnHeight + 10 * scale;

			GUI.Label (new Rect ((Screen.width - btnWidth) / 2, btnTop+5, 100, btnHeight), "验证码");

			code = GUI.TextField(new Rect((Screen.width - btnWidth) / 2 + 100, btnTop, btnWidth - 100, btnHeight), code);

			btnTop += btnHeight + 10 * scale;

			if (GUI.Button(new Rect((Screen.width - btnWidth) / 2, btnTop, btnWidth, btnHeight), "GetCodeSMS"))
			{
				smssdk.getCode (CodeType.TextCode, phone, zone);
			}

			btnTop += btnHeight + 10 * scale;
			if (GUI.Button(new Rect((Screen.width - btnWidth) / 2, btnTop, btnWidth, btnHeight), "CommitCode"))
			{
				smssdk.commitCode (phone, "86",code);
			}

			btnTop += btnHeight + 10 * scale;
			if (GUI.Button(new Rect((Screen.width - btnWidth) / 2, btnTop, btnWidth, btnHeight), "GetCodeVoice"))
			{

				smssdk.getCode (CodeType.VoiceCode, phone, zone);
			}
				
			btnTop += btnHeight + 10 * scale;
			if (GUI.Button(new Rect((Screen.width - btnWidth) / 2, btnTop, btnWidth, btnHeight), "GetCountryCode"))
			{
				smssdk.getSupportedCountryCode ();
			}


			btnTop += btnHeight + 10 * scale;
			if (GUI.Button(new Rect((Screen.width - btnWidth) / 2, btnTop, btnWidth, btnHeight), "GetFriends"))
			{

				smssdk.getFriends ();
			}

			btnTop += btnHeight + 10 * scale;
			if (GUI.Button(new Rect((Screen.width - btnWidth) / 2, btnTop, btnWidth, btnHeight), "SubmitUserInfo"))
			{
				userInfo.avatar = "www.mob.com";
				userInfo.phoneNumber = phone;
				userInfo.zone = zone;
				userInfo.nickName = "David";
				userInfo.uid = "1234567890";
				smssdk.submitUserInfo (userInfo);
			}

			btnTop += btnHeight + 10 * scale;
			if (GUI.Button(new Rect((Screen.width - btnWidth) / 2, btnTop, btnWidth, btnHeight), "GetVersionNumber"))
			{

				smssdk.getVersion ();
			}

			btnTop += btnHeight + 10 * scale;
			if (GUI.Button(new Rect((Screen.width - btnWidth) / 2, btnTop, btnWidth, btnHeight), "EnableWarn"))
			{
				smssdk.enableWarn (true);
			}

			//展示register UI界面
			btnTop += btnHeight + 10 * scale;
			if (GUI.Button(new Rect((Screen.width - btnWidth) / 2, btnTop, btnWidth, btnHeight), "showRegisterUIView"))
			{
				smssdk.showRegisterPage (CodeType.TextCode);
			}

			//展示contractFriends UI界面
			btnTop += btnHeight + 10 * scale;
			if (GUI.Button(new Rect((Screen.width - btnWidth) / 2, btnTop, btnWidth, btnHeight), "showContractsUIView"))
			{
				smssdk.showContactsPage ();
			}
			//展示回调结果
			btnTop += btnHeight + 10 * scale;
			GUI.Label(new Rect((Screen.width - btnWidth) / 2, btnTop, btnWidth, btnHeight), result);
		}

		public void onComplete(int action, object resp)
		{
			ActionType act = (ActionType)action;
			if (resp != null)
			{
				result = resp.ToString();
			}
			if (act == ActionType.GetCode) {
				string responseString = (string)resp;
				Debug.Log ("isSmart :" + responseString);
			} else if (act == ActionType.GetVersion) {
				string version = (string)resp;
				Debug.Log ("version :" + version);
				print ("Demo*version*********" + version);

			} else if (act == ActionType.GetSupportedCountries) {

				string responseString = (string)resp;
				Debug.Log ("zoneString :" + responseString);

			} else if (act == ActionType.GetFriends) {
				string responseString = (string)resp;
				Debug.Log ("friendsString :" + responseString);

			} else if (act == ActionType.CommitCode) {

				string responseString = (string)resp;
				Debug.Log ("commitCodeString :" + responseString);

			} else if (act == ActionType.SubmitUserInfo) {

				string responseString = (string)resp;
				Debug.Log ("submitString :" + responseString);

			} else if (act == ActionType.ShowRegisterView) {

				string responseString = (string)resp;
				Debug.Log ("showRegisterView :" + responseString);

			} else if (act == ActionType.ShowContractFriendsView) {

				string responseString = (string)resp;
				Debug.Log ("showContractFriendsView :" + responseString);
			}
		}

		public void onError(int action, object resp)
		{
			Debug.Log("Error :" + resp);
			result = resp.ToString();
			print ("OnError ******resp"+resp);
		}
	}
}
