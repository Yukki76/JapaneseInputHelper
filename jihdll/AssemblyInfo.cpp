#include "pch.h"

using namespace System;
using namespace System::Reflection;
using namespace System::Runtime::CompilerServices;
using namespace System::Runtime::InteropServices;
using namespace System::Security::Permissions;
using namespace System::Resources;

// �A�Z���u���Ɋւ����ʏ��́A���̈�A�̑����ɂ���Đ��䂳��܂��B
// �A�Z���u���Ɋ֘A�t����ꂽ����ύX����ɂ́A�����̑����l��ύX���܂��B
[assembly:AssemblyTitleAttribute(L"jihdll")];
[assembly:AssemblyDescriptionAttribute(L"")] ;
[assembly:AssemblyConfigurationAttribute(L"")] ;
[assembly:AssemblyCompanyAttribute(L"")] ;
[assembly:AssemblyProductAttribute(L"DLL for JapaneseInputHelper")] ;
[assembly:AssemblyCopyrightAttribute(L"Copyright (C) 2024 Yukki")] ;
[assembly:AssemblyTrademarkAttribute(L"")] ;
[assembly:AssemblyCultureAttribute(L"")] ;
[assembly:NeutralResourcesLanguageAttribute(L"ja-JP")] ;

// ComVisible��false�ɐݒ肷��ƁA���̃A�Z���u�����̌^��COM�R���|�[�l���g�ɕ\������Ȃ��Ȃ�܂��B
// COM���炱�̃A�Z���u�����̌^�ɃA�N�Z�X����K�v������ꍇ�́A���̌^��ComVisible������true�ɐݒ肵�܂��B
[assembly:ComVisible(false)];

// ���� GUID �́A���̃v���W�F�N�g�� COM �Ɍ��J����Ă���ꍇ�� typelib �� ID �ł��B
[assembly:Guid(L"206BCA23-64D3-4E61-B522-64EF4988BE63")] ;

// �A�Z���u���̃o�[�W�������́A����4�̒l�ō\������܂��B
//
//      ���W���[�o�[�W����
//      �}�C�i�[�o�[�W����
//      �r���h�ԍ�
//      ���r�W����
//
// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
// ��: �o�[�W������ύX����Ƃ��́A�K���Z�b�g�A�b�v �v���W�F�N�g�̃o�[�W������
// �X�V���Ă��������B
// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
[assembly:AssemblyVersionAttribute(L"1.0.0.0")];
[assembly:AssemblyFileVersionAttribute(L"1.0.0.0")] ;
