#if !defined(_VMULTI_CLIENT_H_)
#define _VMULTI_CLIENT_H_

#include "vmulticommon.h"

typedef struct _vmulti_client_t* pvmulti_client;

__declspec(dllexport) pvmulti_client vmulti_alloc(void);

__declspec(dllexport) void vmulti_free(pvmulti_client vmulti);

__declspec(dllexport) BOOL vmulti_connect(pvmulti_client vmulti,int i);

__declspec(dllexport) void vmulti_disconnect(pvmulti_client vmulti);

__declspec(dllexport) BOOL vmulti_update_mouse(pvmulti_client vmulti, BYTE button, USHORT x, USHORT y, BYTE wheelPosition);

__declspec(dllexport) BOOL vmulti_update_relative_mouse(pvmulti_client vmulti, BYTE button, BYTE x, BYTE y, BYTE wheelPosition);

__declspec(dllexport) BOOL vmulti_update_digi(pvmulti_client vmulti, BYTE status, USHORT x, USHORT y);

__declspec(dllexport) BOOL vmulti_update_multitouch(pvmulti_client vmulti, PTOUCH pTouch, BYTE actualCount,BYTE request_type,BYTE report_control_id);

__declspec(dllexport) BOOL vmulti_update_joystick(pvmulti_client vmulti, USHORT buttons, BYTE hat, BYTE x, BYTE y, BYTE z, BYTE rz, BYTE throttle);

__declspec(dllexport) BOOL vmulti_update_keyboard(pvmulti_client vmulti, BYTE shiftKeyFlags, BYTE keyCodes[KBD_KEY_CODES]);

__declspec(dllexport) BOOL vmulti_write_message(pvmulti_client vmulti, VMultiMessageReport* pReport);

__declspec(dllexport) BOOL vmulti_read_message(pvmulti_client vmulti, VMultiMessageReport* pReport);

#endif
