// ELF Viewer dll.h

#pragma once

#include <iostream>
#include <cstdio>
#include <cstdlib>
#include <cstddef>
#include <cstring>

static FILE* elf;

using namespace System;

namespace ELFViewer {

	public class ELF32_header		//32bit ELF ����� ����ü ������ �����ϱ� ���� ����ü
	{
	private:
		unsigned char magic[16];
		short type;
		short machine;
		int version;
		int entry;					//Entry Point
		int phoff;					//Program Header Offset
		int shoff;					//Section Header Offset	
		int flags;
		short ehsize;				//ELF Header ũ��
		short phentsize;			//Program Header ũ��
		short phnum;				//Program Header ����
		short shentsize;			//Section Header ũ��
		short shnum;				//Section Header ����
		short shstrndx;

	public:
	//offsetof(ELF32_header, magic) ��ȯ ������ offset ��
	/* �Լ� : ��� ��ȯ �Լ�
	 * ��� : ����� ȣ���� ��� ����� ���� ����
	 * ��ȯ : const int Or short
	 */
		const unsigned char* outmagic(void) const
		{
			return magic;
		}
		const short outtype(void) const
		{
		return type;
		}
		const short outmachine(void) const
		{
			return machine;
		}
		const int outversion(void) const
		{
			return version;
		}
		const int outentry(void) const
		{
			return entry;
		}
		const int outphoff(void) const
		{
			return phoff;
		}
		const int outshoff(void) const
		{
			return shoff;
		}
		const int outflags(void) const
		{
			return flags;
		}
		const short outehsize(void) const
		{
			return ehsize;
		}
		const short outphsize(void) const
		{
			return phentsize;
		}
		const short outphnum(void) const
		{
			return phnum;
		}
		const short outshsize(void) const
		{
			return shentsize;
		}
		const short outshnum(void) const
		{
			return shnum;
		}
		const short outshstrndx(void) const
		{
			return shstrndx;
		}

	/* �Լ� : elf32output
	* ��� : elf ����ü�� ����� ���� ���� ���뿡 ���� ���� ���, �ӽ÷� ������̶� break�� ���� �ص�.
	* ��ȯ : char*
	*/
		char* elf32output_data_endian(char endian);
		char* elf32output_file_version(char f_version);
		char* elf32output_type(short type);
		char* elf32output_machine(short machine);
		char* elf32output_version(int version);
	};

/*--------------------------------64bit ELF ���------------------------------------*/
	public class ELF64_header		//64bit ELF ����� ����ü ������ �����ϱ� ���� ����ü
	{
	private:
		unsigned char magic[16];
		short type;
		short machine;
		int version;
		__int64 entry;					//Entry Point
		__int64 phoff;					//Program Header Offset
		__int64 shoff;					//Section Header Offset
		int flags;
		short ehsize;					//ELF Header ũ��
		short phentsize;				//Program Header ũ��
		short phnum;					//Program Header ����
		short shentsize;				//Section Header ũ��
		short shnum;					//Section Header ����
		short shstrndx;

	public:
	/* �Լ� : ��� ��ȯ �Լ�
	 * ��� : ����� ȣ���� ��� ����� ���� ����
	 * ��ȯ : const int Or short
	 */
		const unsigned char* outmagic(void) const
		{
			return magic;
		}
		const short outtype(void) const
		{
			return type;
		}
		const short outmachine(void) const
		{
			return machine;
		}
		const int outversion(void) const
		{
			return version;
		}
		const __int64 outentry(void) const
		{
			return entry;
		}
		const __int64 outphoff(void) const
		{
			return phoff;
		}
		const __int64 outshoff(void) const
		{
			return shoff;
		}
		const int outflags(void) const
		{
			return flags;
		}
		const short outehsize(void) const
		{
			return ehsize;
		}
		const short outphsize(void) const
		{
			return phentsize;
		}
		const short outphnum(void) const
		{
			return phnum;
		}
		const short outshsize(void) const
		{
			return shentsize;
		}
		const short outshnum(void) const
		{
			return shnum;
		}
		const short outshstrndx(void) const
		{
			return shstrndx;
		}

	/* �Լ� : elf64output
	 * ��� : elf ����ü�� ����� ���� ���� ���뿡 ���� ���� ���, �ӽ÷� ������̶� break�� ���� �ص�.
	 * ��ȯ : char*
	 */
		char* elf64output_data_endian(char endian);
		char* elf64output_file_version(char f_version);
		char* elf64output_type(short type);
		char* elf64output_machine(short machine);
		char* elf64output_version(int version);
	};

/*--------------------------------32bit Program ���------------------------------------*/
	public class Program32_header	//ELF ������ ���α׷� ����� �����ϱ� ���� ����ü
	{
	private:
		int type;
		int offset;
		int vaddr;
		int paddr;
		int filesz;
		int memsz;
		int flags;
		int align;

	public:
	/* �Լ� : ��� ��ȯ �Լ�
	 * ��� : ����� ȣ���� ��� ����� ���� ����
	 * ��ȯ : const int Or short
	 */
		const int outtype(void) const
		{
			return type;
		}
		const int outoffset(void) const
		{
			return offset;
		}
		const int outvaddr(void) const
		{
			return vaddr;
		}
		const int outpaddr(void) const
		{
			return paddr;
		}
		const int outfilesz(void) const
		{
			return filesz;
		}
		const int outmemsz(void) const
		{
			return memsz;
		}
		const int outflags(void) const
		{
			return flags;
		}
		const int outalign(void) const
		{
			return align;
		}
	/* �Լ� : program32output
	 * ��� : program ����ü�� ����� ���� ���� ���뿡 ���� ���� ���
	 * ��ȯ : char*
	 */
		char* program32output_type(int type);
		char* program32output_flags(int flags);
	};

/*--------------------------------64bit Program ���------------------------------------*/
	public class Program64_header	//ELF ������ ���α׷� ����� �����ϱ� ���� ����ü
	{
	private:
		int type;
		int flags;
		__int64 offset;
		__int64 vaddr;
		__int64 paddr;
		__int64 filesz;
		__int64 memsz;
		__int64 align;

	public:
	/* �Լ� : ��� ��ȯ �Լ�
	 * ��� : ����� ȣ���� ��� ����� ���� ����
	 * ��ȯ : const int Or short
	 */
		const int outtype(void) const
		{
			return type;
		}
		const int outflags(void) const
		{
			return flags;
		}
		const __int64 outoffset(void) const
		{
			return offset;
		}
		const __int64 outvaddr(void) const
		{
			return vaddr;
		}
		const __int64 outpaddr(void) const
		{
			return paddr;
		}
		const __int64 outfilesz(void) const
		{
			return filesz;
		}
		const __int64 outmemsz(void) const
		{
			return memsz;
		}
		const __int64 outalign(void) const
		{
			return align;
		}
	/* �Լ� : program64output
	 * ��� : program ����ü�� ����� ���� ���� ���뿡 ���� ���� ���
	 * ��ȯ : char*
	 */
		char* program64output_type(int type);
		char* program64output_flags(int flags);
	};

/*--------------------------------32bit Section ���------------------------------------*/
	public class Section32_header	//ELF ������ ���� ����� �����ϱ� ���� ����ü
	{
	private:
		int name;
		int type;
		int flags;
		int addr;
		int offset;
		int size;
		int link;
		int info;
		int addralign;
		int entsize;

	public:
	/* �Լ� : ��� ��ȯ �Լ�
	 * ��� : ����� ȣ���� ��� ����� ���� ����
	 * ��ȯ : const int Or short
	 */
		const int outname(void) const
		{
			return name;
		}
		const int outtype(void) const
		{
			return type;
		}
		const int outflags(void) const
		{
			return flags;
		}
		const int outaddr(void) const
		{
			return addr;
		}
		const int outoffset(void) const
		{
			return offset;
		}
		const int outsize(void) const
		{
			return size;
		}
		const int outlink(void) const
		{
			return link;
		}
		const int outinfo(void) const
		{
			return info;
		}
		const int outaddralign(void) const
		{
			return addralign;
		}
		const int outentsize(void) const
		{
			return entsize;
		}
	/* �Լ� : section32output
	* ��� : 32bit section ����ü�� ����� ���� ���� ���뿡 ���� ���� ���
	* ��ȯ : char*
	*/
		char* section32output_name(int name);
		char* section32output_type(int type);
		char* section32output_flags(int flags);
	};

/*--------------------------------64bit Section ���------------------------------------*/
	public class Section64_header	//ELF ������ ���� ����� �����ϱ� ���� ����ü
	{
	private:
		int name;
		int type;
		__int64 flags;
		__int64 addr;
		__int64 offset;
		__int64 size;
		int link;
		int info;
		__int64 addralign;
		__int64 entsize;

	public:
	/* �Լ� : ��� ��ȯ �Լ�
	 * ��� : ����� ȣ���� ��� ����� ���� ����
	* ��ȯ : const int Or short
	*/
		const int outname(void) const
		{
			return name;
		}
		const int outtype(void) const
		{
			return type;
		}
		const __int64 outflags(void) const
		{
			return flags;
		}
		const __int64 outaddr(void) const
		{
			return addr;
		}
		const __int64 outoffset(void) const
		{
			return offset;
		}
		const __int64 outsize(void) const
		{
			return size;
		}
		const int outlink(void) const
		{
			return link;
		}
		const int outinfo(void) const
		{
			return info;
		}
		const __int64 outaddralign(void) const
		{
			return addralign;
		}
		const __int64 outentsize(void) const
		{
			return entsize;
		}
	/* �Լ� : section64output
	 * ��� : 64bit section ����ü�� ����� ���� ���� ���뿡 ���� ���� ���
	 * ��ȯ : char*
	 */
		char* section64output_name(int name);
		char* section64output_type(int type);
		char* section64output_flags(__int64 flags);
	};

/*-------------------------����ü ����� ���� ����ü ����(�޸� ���� �ذ�)---------------------------*/
	union ELF_header
	{
		ELF32_header e32_header;
		ELF64_header e64_header;
	};

	union Program_header
	{
		Program32_header p32_header[20];
		Program64_header p64_header[20];
	};

	union Section_header
	{
		Section32_header s32_header[40];
		Section64_header s64_header[40];
	};

/*------------------------------------����� ���� �ֻ����� �θ�Ŭ����-----------------------------------*/
	public class Header
	{
/*-----------------------����ü ����-------------------------*/
	private:
		union ELF_header e_header;
		union Program_header p_header;						//���α׷� ����� ������ ������ �̹Ƿ� �迭�� ����
		union Section_header s_header;						//���� ����, �ٸ� ������ ���� �̻��� �� ��� ���� �߻��ϹǷ� ��å �ʿ�

	protected:
	
	public:
/*-----------------------��� ����-------------------------*/
		int fsize, offset32, error, i;
		__int64 offset64;
		char* data;
		const char bitcheck;

/*-----------------------��� �Լ�-------------------------*/
		Header() : fsize(0), offset32(0), offset64(0), bitcheck((char)0x01) {			//����Ʈ ������
			elf = 0x0;
		}			

	/* �Լ� : fileopen
	 * ��� : ELF ���� ���̳ʸ� ���·� ����
	 * ��ȯ : int
	 */
		int fileopen(const char* filename)
		{
			fopen_s(&elf, filename, "rb");							//���ڷ� ���� ELF ���� ���� 
	
			if(!elf)												//���� ���� ����, ���� Ȯ�� �ʿ�
			{
				//���� ���
	
				exit(1);											//������ ���� �߻�, ��������
			}

			fseek(elf, 0, SEEK_END);
			fsize = ftell(elf);										//fseek �Լ��� ���� ������ ������ �̵��� �� ������ ũ�� ����
			rewind(elf);											//������ ó�� �κ����� ����

			if((data=(char*)malloc(fsize*sizeof(char))) == 0)		//������ ũ�⸸ŭ �� ���� �Ҵ� ����
				//���� ���

			error = fread_s(data,fsize,fsize,sizeof(char),elf);		//fread�� ���� ������ ���� ���� �� ������ ����
			if(error == fsize)
				perror("Failed to read file.\n");

			if(memcmp(data+1, "ELF", 3))							//ELF ���� üũ(ELF�� �̻��� Ư�����ڰ� �����Ƿ� 1ĭ ��� 3���� Ȯ��)
			{
				//���� ���

				exit(1);											//������ ���� �߻�, ��������
			}

			if(!memcmp(data+4, &bitcheck, 1))		//bit üũ, 32bit�� �� �ٷ� �Ʒ�, �ƴ� �� else
			{
				return 0x01;
			}
			else									//64bit
			{
				return 0x02;
			}
		}

	/* �Լ� : _32input
	 * ��� : 32bit ELF ���� ���� ����ü �޸𸮿� ����
	 * ��ȯ : void
	 */
		void _32input(void)
		{
			memcpy_s(&e_header.e32_header, sizeof(e_header.e32_header), data, sizeof(e_header.e32_header));		//32bit ELF ������ ELF header�� �����Ͽ� ����ü�� ����
		
			offset32 = e_header.e32_header.outphoff();
			for(i=0; i<(e_header.e32_header.outphnum()); i++)											//32bit ELF ������ Program header�� ����
			{
				memcpy_s(&p_header.p32_header[i], e_header.e32_header.outphsize(), (data + offset32), e_header.e32_header.outphsize());
				offset32 += e_header.e32_header.outphsize();
			}

			offset32 = e_header.e32_header.outshoff();
			for(i=0; i<(e_header.e32_header.outshnum()); i++)											//32bit ELF ������ Section header�� ����
			{
				memcpy_s(&s_header.s32_header[i], e_header.e32_header.outshsize(), (data + offset32), e_header.e32_header.outshsize());
				offset32 += e_header.e32_header.outshsize();
			}

			free(data);					//��ü ���� �Ϸ�Ǿ����� ������ ����
		}

	/* �Լ� : _64input
	 * ��� : 64bit ELF ���� ���� ����ü �޸𸮿� ����
	 * ��ȯ : void
	 */
		void _64input(void)
		{
			memcpy_s(&e_header.e64_header, sizeof(e_header), data, sizeof(e_header));		//64bit ELF ������ ELF header�� �����Ͽ� ����ü�� ����

			offset64 = e_header.e64_header.outphoff();
			for(i=0; i<(e_header.e64_header.outphnum()); i++)
			{
				memcpy_s(&p_header.p64_header[i], e_header.e64_header.outphsize(), (data + offset64), e_header.e64_header.outphsize());
				offset64 += e_header.e64_header.outphsize();
			}

			offset64 = e_header.e64_header.outshoff();
			for(i=0; i<(e_header.e64_header.outshnum()); i++)
			{
				memcpy_s(&s_header.s64_header[i], e_header.e64_header.outshsize(), (data + offset64), e_header.e64_header.outshsize());
				offset64 += e_header.e64_header.outshsize();
			}

			free(data);					//��ü ���� �Ϸ�Ǿ����� ������ ����
		}

		~Header()													//���α׷� ���Ḧ ��Ÿ���Ƿ� ���� ���Ḧ ���� �Ҹ���
		{
			try														//elf ������ ������� �ʾ��� ��� elf
			{
				if(elf == NULL)
					throw -1;
				fclose(elf);
			}
			catch(int exception)
			{
				exception = 0;
	
				exit(0);
			}
		}
	};
}
