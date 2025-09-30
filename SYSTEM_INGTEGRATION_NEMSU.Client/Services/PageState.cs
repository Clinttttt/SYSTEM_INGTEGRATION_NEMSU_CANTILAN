﻿using Microsoft.Data.SqlClient;

namespace SYSTEM_INGTEGRATION_NEMSU.Client.Services
{
    public class PageState
    {
        public event Action? Onchange;

        public SaveAsDraftPerInfo CurrentViewPerInfo { get; set; } = SaveAsDraftPerInfo.ViewInfo;
        public SaveAsDraftAcads CurrentViewAcads { get; set; } = SaveAsDraftAcads.ViewInfo;
        public SaveAsDraftContact CurrentViewContact { get; set; } = SaveAsDraftContact.ViewInfo;
        public enum SaveAsDraftPerInfo
        {
            ViewInfo,
            SaveASDraft,
            Save
        }
        public enum SaveAsDraftAcads
        {
            ViewInfo,
            SaveASDraft,
            Save
        }
        public enum SaveAsDraftContact
        {
            ViewInfo,
            SaveASDraft,
            Save
        }
      
        public void SetViewPerInfo(SaveAsDraftPerInfo View)
        {
            CurrentViewPerInfo = View;
            Onchange?.Invoke();
        }
     
        public void SetViewAcads(SaveAsDraftAcads View)
        {
            CurrentViewAcads = View;
            Onchange?.Invoke();
        }
        public void SetViewContact(SaveAsDraftContact View)
        {
            CurrentViewContact = View;
            Onchange?.Invoke();
        }
        public Timeline CurrentViewTimeline { get; set; } = Timeline.Fillout;
        public enum Timeline
        {
            Fillout,
            LoginReady
        }
        public void SetView(Timeline view)
        {
            CurrentViewTimeline = view;
            Onchange?.Invoke();
        }

    }
}
